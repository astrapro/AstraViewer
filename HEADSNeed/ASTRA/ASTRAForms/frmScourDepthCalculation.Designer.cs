﻿namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmScourDepthCalculation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDrawing = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnWorkingFolder = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_F2 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txt_Ksb = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_L = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_F1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txt_n = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_S = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_V1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_SDO = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_LBL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_LWL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_HFL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRiver = new System.Windows.Forms.DataGridView();
            this.SL_N = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reduce = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiver)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDrawing);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Controls.Add(this.btnWorkingFolder);
            this.panel1.Location = new System.Drawing.Point(252, 320);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 28);
            this.panel1.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(401, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDrawing
            // 
            this.btnDrawing.Enabled = false;
            this.btnDrawing.Location = new System.Drawing.Point(268, 1);
            this.btnDrawing.Name = "btnDrawing";
            this.btnDrawing.Size = new System.Drawing.Size(77, 23);
            this.btnDrawing.TabIndex = 3;
            this.btnDrawing.Text = "Drawing";
            this.btnDrawing.UseVisualStyleBackColor = true;
            this.btnDrawing.Click += new System.EventHandler(this.btnDrawing_Click);
            // 
            // btnReport
            // 
            this.btnReport.Enabled = false;
            this.btnReport.Location = new System.Drawing.Point(187, 1);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Enabled = false;
            this.btnProcess.Location = new System.Drawing.Point(108, 1);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 1;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnWorkingFolder
            // 
            this.btnWorkingFolder.Location = new System.Drawing.Point(7, 1);
            this.btnWorkingFolder.Name = "btnWorkingFolder";
            this.btnWorkingFolder.Size = new System.Drawing.Size(88, 23);
            this.btnWorkingFolder.TabIndex = 0;
            this.btnWorkingFolder.Text = "Working Folder";
            this.btnWorkingFolder.UseVisualStyleBackColor = true;
            this.btnWorkingFolder.Click += new System.EventHandler(this.btnWorkingFolder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_F2);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.txt_Ksb);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txt_L);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.txt_F1);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txt_n);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txt_S);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txt_V1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txt_SDO);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt_LBL);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_LWL);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_HFL);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 302);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "USER\'S DATA";
            // 
            // txt_F2
            // 
            this.txt_F2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_F2.Location = new System.Drawing.Point(582, 271);
            this.txt_F2.Name = "txt_F2";
            this.txt_F2.Size = new System.Drawing.Size(83, 22);
            this.txt_F2.TabIndex = 33;
            this.txt_F2.Text = "1.33";
            this.txt_F2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(333, 275);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(246, 13);
            this.label24.TabIndex = 32;
            this.label24.Text = "Factor for Foundation depth over Scour Depth [F2]";
            // 
            // txt_Ksb
            // 
            this.txt_Ksb.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Ksb.Location = new System.Drawing.Point(582, 243);
            this.txt_Ksb.Name = "txt_Ksb";
            this.txt_Ksb.Size = new System.Drawing.Size(83, 22);
            this.txt_Ksb.TabIndex = 30;
            this.txt_Ksb.Text = "1.0";
            this.txt_Ksb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(333, 247);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(81, 13);
            this.label22.TabIndex = 29;
            this.label22.Text = "Silt Factor [Ksb]";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(671, 218);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 14);
            this.label19.TabIndex = 28;
            this.label19.Text = "m";
            // 
            // txt_L
            // 
            this.txt_L.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_L.Location = new System.Drawing.Point(582, 215);
            this.txt_L.Name = "txt_L";
            this.txt_L.Size = new System.Drawing.Size(83, 22);
            this.txt_L.TabIndex = 27;
            this.txt_L.Text = "37.5";
            this.txt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(333, 218);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(121, 13);
            this.label20.TabIndex = 26;
            this.label20.Text = "Proposed Water way [L]";
            // 
            // txt_F1
            // 
            this.txt_F1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_F1.Location = new System.Drawing.Point(582, 191);
            this.txt_F1.Name = "txt_F1";
            this.txt_F1.Size = new System.Drawing.Size(83, 22);
            this.txt_F1.TabIndex = 24;
            this.txt_F1.Text = "1.3";
            this.txt_F1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(333, 195);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(227, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "Factor for Discharge for Foundation Depth [F1]";
            // 
            // txt_n
            // 
            this.txt_n.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_n.Location = new System.Drawing.Point(582, 163);
            this.txt_n.Name = "txt_n";
            this.txt_n.Size = new System.Drawing.Size(83, 22);
            this.txt_n.TabIndex = 21;
            this.txt_n.Text = "0.03";
            this.txt_n.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(333, 168);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(55, 13);
            this.label16.TabIndex = 20;
            this.label16.Text = "Value of n";
            // 
            // txt_S
            // 
            this.txt_S.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_S.Location = new System.Drawing.Point(582, 135);
            this.txt_S.Name = "txt_S";
            this.txt_S.Size = new System.Drawing.Size(83, 22);
            this.txt_S.TabIndex = 18;
            this.txt_S.Text = "0.000769";
            this.txt_S.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(333, 139);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(90, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Slope of River [S]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(671, 110);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 16;
            this.label11.Text = "m/sec";
            // 
            // txt_V1
            // 
            this.txt_V1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_V1.Location = new System.Drawing.Point(582, 107);
            this.txt_V1.Name = "txt_V1";
            this.txt_V1.Size = new System.Drawing.Size(83, 22);
            this.txt_V1.TabIndex = 15;
            this.txt_V1.Text = "2.20";
            this.txt_V1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(333, 111);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Oberved Velocity [V1]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(671, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "m";
            // 
            // txt_SDO
            // 
            this.txt_SDO.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SDO.Location = new System.Drawing.Point(582, 79);
            this.txt_SDO.Name = "txt_SDO";
            this.txt_SDO.Size = new System.Drawing.Size(83, 22);
            this.txt_SDO.TabIndex = 12;
            this.txt_SDO.Text = "5.40";
            this.txt_SDO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(333, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Scour Depth Observed [SDO]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(671, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "m";
            // 
            // txt_LBL
            // 
            this.txt_LBL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LBL.Location = new System.Drawing.Point(582, 51);
            this.txt_LBL.Name = "txt_LBL";
            this.txt_LBL.Size = new System.Drawing.Size(83, 22);
            this.txt_LBL.TabIndex = 9;
            this.txt_LBL.Text = "61.970";
            this.txt_LBL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(333, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Lowest Bed Level [LBL]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(671, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "m";
            // 
            // txt_LWL
            // 
            this.txt_LWL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_LWL.Location = new System.Drawing.Point(582, 23);
            this.txt_LWL.Name = "txt_LWL";
            this.txt_LWL.Size = new System.Drawing.Size(83, 22);
            this.txt_LWL.TabIndex = 6;
            this.txt_LWL.Text = "62.570";
            this.txt_LWL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Low Water Level [LWL]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(237, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "m";
            // 
            // txt_HFL
            // 
            this.txt_HFL.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_HFL.Location = new System.Drawing.Point(148, 23);
            this.txt_HFL.Name = "txt_HFL";
            this.txt_HFL.Size = new System.Drawing.Size(83, 22);
            this.txt_HFL.TabIndex = 3;
            this.txt_HFL.Text = "67.370";
            this.txt_HFL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "High Flood Level [HFL]";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvRiver);
            this.groupBox1.Location = new System.Drawing.Point(9, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 242);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "River Bed Cross Section Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 52);
            this.label2.TabIndex = 2;
            this.label2.Text = "Reduced Levels \r\nof the River \r\nBed\r\n(m)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 65);
            this.label1.TabIndex = 1;
            this.label1.Text = "Distance from Reference \r\nPoint to Various points \r\nacross cross section of river" +
                " \r\nat bridge location\r\n(m)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvRiver
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRiver.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRiver.ColumnHeadersHeight = 21;
            this.dgvRiver.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SL_N,
            this.dist,
            this.reduce});
            this.dgvRiver.Location = new System.Drawing.Point(9, 84);
            this.dgvRiver.Name = "dgvRiver";
            this.dgvRiver.RowHeadersWidth = 27;
            this.dgvRiver.Size = new System.Drawing.Size(288, 153);
            this.dgvRiver.TabIndex = 0;
            // 
            // SL_N
            // 
            this.SL_N.HeaderText = "SL.N";
            this.SL_N.Name = "SL_N";
            this.SL_N.ReadOnly = true;
            this.SL_N.Width = 30;
            // 
            // dist
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dist.DefaultCellStyle = dataGridViewCellStyle2;
            this.dist.HeaderText = "(1)";
            this.dist.Name = "dist";
            this.dist.Width = 110;
            // 
            // reduce
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.reduce.DefaultCellStyle = dataGridViewCellStyle3;
            this.reduce.HeaderText = "(2)";
            this.reduce.Name = "reduce";
            this.reduce.Width = 110;
            // 
            // frmScourDepthCalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 354);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "frmScourDepthCalculation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scour Depth Calculation";
            this.Load += new System.EventHandler(this.frmScourDepthCalculation_Load);
            this.Click += new System.EventHandler(this.frmScourDepthCalculation_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRiver)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDrawing;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnWorkingFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_F2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txt_Ksb;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_L;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_F1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_n;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_S;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_V1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_SDO;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_LBL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_LWL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_HFL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvRiver;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL_N;
        private System.Windows.Forms.DataGridViewTextBoxColumn dist;
        private System.Windows.Forms.DataGridViewTextBoxColumn reduce;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}