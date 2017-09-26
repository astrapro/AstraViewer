namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmElements
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btn_elmt_add = new System.Windows.Forms.Button();
            this.grb_start_joints = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_elmt_no = new System.Windows.Forms.TextBox();
            this.txt_elmt_node1 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_elmt_node2 = new System.Windows.Forms.TextBox();
            this.txt_elmt_node4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_elmt_node3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grb_start_joints.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(350, 96);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(153, 33);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_elmt_add
            // 
            this.btn_elmt_add.Location = new System.Drawing.Point(168, 96);
            this.btn_elmt_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_elmt_add.Name = "btn_elmt_add";
            this.btn_elmt_add.Size = new System.Drawing.Size(153, 33);
            this.btn_elmt_add.TabIndex = 1;
            this.btn_elmt_add.Text = "ADD ELEMENT";
            this.btn_elmt_add.UseVisualStyleBackColor = true;
            this.btn_elmt_add.Click += new System.EventHandler(this.btn_elmt_add_Click);
            // 
            // grb_start_joints
            // 
            this.grb_start_joints.Controls.Add(this.label2);
            this.grb_start_joints.Controls.Add(this.label21);
            this.grb_start_joints.Controls.Add(this.txt_elmt_no);
            this.grb_start_joints.Controls.Add(this.txt_elmt_node3);
            this.grb_start_joints.Controls.Add(this.txt_elmt_node1);
            this.grb_start_joints.Controls.Add(this.label1);
            this.grb_start_joints.Controls.Add(this.label23);
            this.grb_start_joints.Controls.Add(this.txt_elmt_node4);
            this.grb_start_joints.Controls.Add(this.label20);
            this.grb_start_joints.Controls.Add(this.txt_elmt_node2);
            this.grb_start_joints.Location = new System.Drawing.Point(3, 13);
            this.grb_start_joints.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grb_start_joints.Name = "grb_start_joints";
            this.grb_start_joints.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grb_start_joints.Size = new System.Drawing.Size(665, 67);
            this.grb_start_joints.TabIndex = 0;
            this.grb_start_joints.TabStop = false;
            this.grb_start_joints.Text = "Plate Element";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(298, 33);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 14);
            this.label21.TabIndex = 1;
            this.label21.Text = "NODE 2";
            // 
            // txt_elmt_no
            // 
            this.txt_elmt_no.Location = new System.Drawing.Point(111, 30);
            this.txt_elmt_no.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_elmt_no.Name = "txt_elmt_no";
            this.txt_elmt_no.Size = new System.Drawing.Size(54, 22);
            this.txt_elmt_no.TabIndex = 0;
            this.txt_elmt_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_elmt_node1
            // 
            this.txt_elmt_node1.Location = new System.Drawing.Point(238, 30);
            this.txt_elmt_node1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_elmt_node1.Name = "txt_elmt_node1";
            this.txt_elmt_node1.Size = new System.Drawing.Size(54, 22);
            this.txt_elmt_node1.TabIndex = 1;
            this.txt_elmt_node1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 33);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(87, 14);
            this.label23.TabIndex = 1;
            this.label23.Text = "ELEMENT NO";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(177, 33);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 14);
            this.label20.TabIndex = 1;
            this.label20.Text = "NODE 1";
            // 
            // txt_elmt_node2
            // 
            this.txt_elmt_node2.Location = new System.Drawing.Point(361, 30);
            this.txt_elmt_node2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_elmt_node2.Name = "txt_elmt_node2";
            this.txt_elmt_node2.Size = new System.Drawing.Size(54, 22);
            this.txt_elmt_node2.TabIndex = 2;
            this.txt_elmt_node2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_elmt_node4
            // 
            this.txt_elmt_node4.Location = new System.Drawing.Point(603, 30);
            this.txt_elmt_node4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_elmt_node4.Name = "txt_elmt_node4";
            this.txt_elmt_node4.Size = new System.Drawing.Size(54, 22);
            this.txt_elmt_node4.TabIndex = 4;
            this.txt_elmt_node4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(419, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "NODE 3";
            // 
            // txt_elmt_node3
            // 
            this.txt_elmt_node3.Location = new System.Drawing.Point(480, 30);
            this.txt_elmt_node3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_elmt_node3.Name = "txt_elmt_node3";
            this.txt_elmt_node3.Size = new System.Drawing.Size(54, 22);
            this.txt_elmt_node3.TabIndex = 3;
            this.txt_elmt_node3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(540, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "NODE 4";
            // 
            // frmElements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 150);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btn_elmt_add);
            this.Controls.Add(this.grb_start_joints);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmElements";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elements";
            this.Load += new System.EventHandler(this.frmElements_Load);
            this.grb_start_joints.ResumeLayout(false);
            this.grb_start_joints.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btn_elmt_add;
        private System.Windows.Forms.GroupBox grb_start_joints;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_elmt_no;
        private System.Windows.Forms.TextBox txt_elmt_node1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_elmt_node2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_elmt_node3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_elmt_node4;
    }
}