using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace HEADSNeed.DisNet
{
    public partial class frmDisNetDetails : Form
    {
        string fileName = "";
        public frmDisNetDetails(string modelTextFile)
        {
            InitializeComponent();
            this.fileName = modelTextFile;
        }

        private void frmDisNetDetails_Load(object sender, EventArgs e)
        {
            textBox1.Lines = File.ReadAllLines(fileName);
        }
    }
}
