namespace ExtraMethods
{
    partial class EditCreateBlock
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
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.vdFramedControl1 = new vdControls.vdFramedControl();
            this.textName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butAddInsertionPoint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butOk
            // 
            this.butOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.butOk.Location = new System.Drawing.Point(293, 443);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(567, 443);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // vdFramedControl1
            // 
            this.vdFramedControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.vdFramedControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vdFramedControl1.DisplayPolarCoord = false;
            this.vdFramedControl1.HistoryLines = ((uint)(3u));
            this.vdFramedControl1.Location = new System.Drawing.Point(11, 15);
            this.vdFramedControl1.Name = "vdFramedControl1";
            this.vdFramedControl1.PropertyGridWidth = ((uint)(300u));
            this.vdFramedControl1.Size = new System.Drawing.Size(867, 421);
            this.vdFramedControl1.TabIndex = 2;
            // 
            // textName
            // 
            this.textName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textName.Location = new System.Drawing.Point(885, 28);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(107, 20);
            this.textName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(918, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // butAddInsertionPoint
            // 
            this.butAddInsertionPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddInsertionPoint.Location = new System.Drawing.Point(884, 54);
            this.butAddInsertionPoint.Name = "butAddInsertionPoint";
            this.butAddInsertionPoint.Size = new System.Drawing.Size(107, 23);
            this.butAddInsertionPoint.TabIndex = 5;
            this.butAddInsertionPoint.Text = "Add Insertion Point";
            this.butAddInsertionPoint.UseVisualStyleBackColor = true;
            this.butAddInsertionPoint.Click += new System.EventHandler(this.butAddInsertionPoint_Click);
            // 
            // EditCreateBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 471);
            this.Controls.Add(this.butAddInsertionPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.vdFramedControl1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.MinimumSize = new System.Drawing.Size(580, 280);
            this.Name = "EditCreateBlock";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "EditCreateBlock";
            this.Load += new System.EventHandler(this.EditCreateBlock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private vdControls.vdFramedControl vdFramedControl1;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butAddInsertionPoint;
    }
}