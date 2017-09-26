namespace HEADSNeed.DrawingToData
{
    partial class frmDrawPolylineFromFile
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
            this.btn_browse = new System.Windows.Forms.Button();
            this.btn_Proceed = new System.Windows.Forms.Button();
            this.btn_Finish = new System.Windows.Forms.Button();
            this.grb_Poly_To_Text = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_layer_label = new System.Windows.Forms.DataGridView();
            this.col_layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grb_data_to_poly = new System.Windows.Forms.GroupBox();
            this.lbl_moredata = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_select_file = new System.Windows.Forms.TextBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.pnl_buttons = new System.Windows.Forms.Panel();
            this.grb_Poly_To_Text.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_layer_label)).BeginInit();
            this.grb_data_to_poly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.pnl_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_browse
            // 
            this.btn_browse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browse.Location = new System.Drawing.Point(612, 15);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(33, 23);
            this.btn_browse.TabIndex = 1;
            this.btn_browse.Text = "....";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // btn_Proceed
            // 
            this.btn_Proceed.Location = new System.Drawing.Point(13, 12);
            this.btn_Proceed.Name = "btn_Proceed";
            this.btn_Proceed.Size = new System.Drawing.Size(75, 23);
            this.btn_Proceed.TabIndex = 13;
            this.btn_Proceed.Text = "Next";
            this.btn_Proceed.UseVisualStyleBackColor = true;
            this.btn_Proceed.Click += new System.EventHandler(this.btn_Proceed_Click);
            // 
            // btn_Finish
            // 
            this.btn_Finish.Location = new System.Drawing.Point(169, 12);
            this.btn_Finish.Name = "btn_Finish";
            this.btn_Finish.Size = new System.Drawing.Size(75, 23);
            this.btn_Finish.TabIndex = 14;
            this.btn_Finish.Text = "Close";
            this.btn_Finish.UseVisualStyleBackColor = true;
            this.btn_Finish.Click += new System.EventHandler(this.btn_Finish_Click);
            // 
            // grb_Poly_To_Text
            // 
            this.grb_Poly_To_Text.Controls.Add(this.label1);
            this.grb_Poly_To_Text.Controls.Add(this.dgv_layer_label);
            this.grb_Poly_To_Text.Location = new System.Drawing.Point(12, 8);
            this.grb_Poly_To_Text.Name = "grb_Poly_To_Text";
            this.grb_Poly_To_Text.Size = new System.Drawing.Size(257, 289);
            this.grb_Poly_To_Text.TabIndex = 17;
            this.grb_Poly_To_Text.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 84);
            this.label1.TabIndex = 21;
            this.label1.Text = "User is advised to always change \r\nthe label for respective feature.\r\n\r\nFor Examp" +
                "le, \r\nLayer = 3d Shoulder, Label = ES\r\nLayer = Electric Pole, Label = EP\r\n";
            // 
            // dgv_layer_label
            // 
            this.dgv_layer_label.AllowUserToAddRows = false;
            this.dgv_layer_label.AllowUserToDeleteRows = false;
            this.dgv_layer_label.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_layer_label.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_layer,
            this.col_label});
            this.dgv_layer_label.Location = new System.Drawing.Point(5, 103);
            this.dgv_layer_label.Name = "dgv_layer_label";
            this.dgv_layer_label.RowHeadersVisible = false;
            this.dgv_layer_label.Size = new System.Drawing.Size(246, 180);
            this.dgv_layer_label.TabIndex = 24;
            // 
            // col_layer
            // 
            this.col_layer.HeaderText = "Layer Name";
            this.col_layer.Name = "col_layer";
            this.col_layer.Width = 130;
            // 
            // col_label
            // 
            this.col_label.HeaderText = "Label";
            this.col_label.Name = "col_label";
            this.col_label.Width = 110;
            // 
            // grb_data_to_poly
            // 
            this.grb_data_to_poly.Controls.Add(this.lbl_moredata);
            this.grb_data_to_poly.Controls.Add(this.btn_browse);
            this.grb_data_to_poly.Controls.Add(this.dataGridView1);
            this.grb_data_to_poly.Controls.Add(this.dataGridView2);
            this.grb_data_to_poly.Controls.Add(this.txt_select_file);
            this.grb_data_to_poly.Location = new System.Drawing.Point(9, 10);
            this.grb_data_to_poly.Name = "grb_data_to_poly";
            this.grb_data_to_poly.Size = new System.Drawing.Size(664, 317);
            this.grb_data_to_poly.TabIndex = 18;
            this.grb_data_to_poly.TabStop = false;
            this.grb_data_to_poly.Text = "Select File Name :";
            // 
            // lbl_moredata
            // 
            this.lbl_moredata.BackColor = System.Drawing.Color.Red;
            this.lbl_moredata.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_moredata.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_moredata.Location = new System.Drawing.Point(8, 264);
            this.lbl_moredata.Name = "lbl_moredata";
            this.lbl_moredata.Size = new System.Drawing.Size(629, 23);
            this.lbl_moredata.TabIndex = 21;
            this.lbl_moredata.Text = "There is more data present in the file.";
            this.lbl_moredata.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_moredata.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dataGridView1.Location = new System.Drawing.Point(6, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(651, 45);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.SizeChanged += new System.EventHandler(this.dataGridView1_SizeChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column10.Name = "Column10";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20});
            this.dataGridView2.Location = new System.Drawing.Point(6, 83);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(651, 230);
            this.dataGridView2.TabIndex = 20;
            this.dataGridView2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView2_Scroll);
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.MaxInputLength = 2147483647;
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Column12";
            this.Column12.MaxInputLength = 2147483647;
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Column13";
            this.Column13.MaxInputLength = 2147483647;
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Column14";
            this.Column14.MaxInputLength = 2147483647;
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.MaxInputLength = 2147483647;
            this.Column15.Name = "Column15";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Column16";
            this.Column16.MaxInputLength = 2147483647;
            this.Column16.Name = "Column16";
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Column17";
            this.Column17.Name = "Column17";
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Column18";
            this.Column18.Name = "Column18";
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Column19";
            this.Column19.Name = "Column19";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Column20";
            this.Column20.Name = "Column20";
            // 
            // txt_select_file
            // 
            this.txt_select_file.Location = new System.Drawing.Point(10, 18);
            this.txt_select_file.Name = "txt_select_file";
            this.txt_select_file.Size = new System.Drawing.Size(596, 20);
            this.txt_select_file.TabIndex = 0;
            this.txt_select_file.TextChanged += new System.EventHandler(this.txt_select_file_TextChanged);
            // 
            // sfd
            // 
            this.sfd.Filter = "Space(\' \') Separated Files [*.txt]|*.txt|Comma(\',\') Separated File [*.csv]|*.csv";
            // 
            // pnl_buttons
            // 
            this.pnl_buttons.Controls.Add(this.btn_Finish);
            this.pnl_buttons.Controls.Add(this.btn_Proceed);
            this.pnl_buttons.Location = new System.Drawing.Point(9, 333);
            this.pnl_buttons.Name = "pnl_buttons";
            this.pnl_buttons.Size = new System.Drawing.Size(254, 43);
            this.pnl_buttons.TabIndex = 19;
            // 
            // frmDrawPolylineFromFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 449);
            this.Controls.Add(this.grb_data_to_poly);
            this.Controls.Add(this.grb_Poly_To_Text);
            this.Controls.Add(this.pnl_buttons);
            this.MaximizeBox = false;
            this.Name = "frmDrawPolylineFromFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmDrawPolylineFromFile";
            this.Load += new System.EventHandler(this.frmDrawPolylineFromFile_Load);
            this.grb_Poly_To_Text.ResumeLayout(false);
            this.grb_Poly_To_Text.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_layer_label)).EndInit();
            this.grb_data_to_poly.ResumeLayout(false);
            this.grb_data_to_poly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.pnl_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Button btn_Proceed;
        private System.Windows.Forms.Button btn_Finish;
        private System.Windows.Forms.GroupBox grb_Poly_To_Text;
        private System.Windows.Forms.TextBox txt_select_file;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.GroupBox grb_data_to_poly;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label lbl_moredata;
        private System.Windows.Forms.Panel pnl_buttons;
        private System.Windows.Forms.DataGridView dgv_layer_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_layer;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column8;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
    }
}