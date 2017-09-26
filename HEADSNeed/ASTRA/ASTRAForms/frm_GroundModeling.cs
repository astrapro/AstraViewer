using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HeadsUtils;
//using HEADSNeed.DESIGN;


namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_GroundModeling : Form
    {
       
         string fPath = "";
         public frm_GroundModeling(string filepath)
        {
            InitializeComponent();
            if (File.Exists(filepath))
                fPath = Path.GetDirectoryName(filepath);
            else
                fPath = filepath;
        }

         public bool ReadModelsFromFile()
         {
             string filePath = fPath;
             bool success = false;
             if (File.Exists(filePath))
                 filePath = Path.GetDirectoryName(filePath);
             BinaryReader br;
             long len = 1;

             filePath = Path.Combine(filePath, "model.lst");
             //LstModel.Clear();

             if (File.Exists(filePath) == false) return false;

             br = new BinaryReader(new FileStream(filePath, FileMode.Open, FileAccess.Read), Encoding.Default);
             try
             {
                 LstModel = new List<CModel>();
                 len = br.BaseStream.Length;
                 CModel cm;
                 while (br.BaseStream.Position < len)
                 {
                     cm = new CModel();
                     cm.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
                     cm.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

                     if (cm.ModelName != "")
                         LstModel.Add(cm);
                 }
                 success = true;
             }
             catch (Exception exx)
             {
                 success = false;
             }
             finally
             {
                 br.Close();
             }
             return success;
         }

        private void btnDeSelectAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lbModelAndStringName.Items.Count; iIndex++)
            {
                this.lbModelAndStringName.SetSelected(iIndex, false);
            }
        }
        public List<CModel> LstModel { get; set; }

        private void frmGroundModeling_Load(object sender, EventArgs e)
        {
            string kStr = "";

            ReadModelsFromFile();
            if (LstModel == null) return;

            for (int i = 0; i < LstModel.Count; i++)
            {
                kStr = String.Format("{0,-25} :{1,-25}", LstModel[i].ModelName,
                    LstModel[i].StringName);
                if (lbModelAndStringName.Items.Contains(kStr) == false)
                    lbModelAndStringName.Items.Add(kStr);
            }
            for (int iIndex = 0; iIndex < this.lbModelAndStringName.Items.Count; iIndex++)
            {
                this.lbModelAndStringName.SetSelected(iIndex, true);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string strSerach = this.txtSelect.Text.Trim();
            if (strSerach != "")
            {
                int iQuesMarkIndex = strSerach.IndexOf('?');
                if (iQuesMarkIndex > 0)
                {
                    strSerach = strSerach.Substring(0, iQuesMarkIndex);
                }

                for (int iIndex = 0; iIndex < this.lbModelAndStringName.Items.Count; iIndex++)
                {
                    string kSS = this.lbModelAndStringName.Items[iIndex].ToString();
                    int idx = kSS.IndexOf(":");


                    kSS = kSS.Substring(idx + 1, kSS.Length - 1 - idx);


                    if (kSS.StartsWith(strSerach))
                    {
                        this.lbModelAndStringName.SetSelected(iIndex, true);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lbModelAndStringName.Items.Count; iIndex++)
            {
                this.lbModelAndStringName.SetSelected(iIndex, true);
            }
        }

        private void btnDeSelect_Click(object sender, EventArgs e)
        {
            string strSerach = this.txtDeSelect.Text.Trim();
            if (strSerach != "")
            {
                int iQuesMarkIndex = strSerach.IndexOf('?');
                if (iQuesMarkIndex > 0)
                {
                    strSerach = strSerach.Substring(0, iQuesMarkIndex);
                }

                for (int iIndex = 0; iIndex < this.lbModelAndStringName.Items.Count; iIndex++)
                {
                    string kSS = this.lbModelAndStringName.Items[iIndex].ToString();
                    int idx = kSS.IndexOf(":");

                    kSS = kSS.Substring(idx + 1, kSS.Length - 1 - idx);
                    if (kSS.StartsWith(strSerach))
                    {
                        this.lbModelAndStringName.SetSelected(iIndex, false);
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbModelAndStringName.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("Please select atleast one Model and string for Ground Modelling", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StreamWriter sw = new StreamWriter(new FileStream(Path.Combine(fPath, "DGM4DLG.TMP"), FileMode.Create));


            string kStr = "";
            int indx = 0;
            try
            {
                for (int i = 0; i < lbModelAndStringName.SelectedIndices.Count; i++)
                {
                    indx = lbModelAndStringName.SelectedIndices[i];
                    kStr = lbModelAndStringName.Items[indx].ToString();

                    string[] vals = kStr.Split(new char[] { ':' });
                    //vals[0] = vals[0].Trim();
                    //vals[1] = vals[1].Trim();

                    sw.Write("{0,-25}{1,-15}", vals[0].Trim(), vals[1].Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Proccessing Error!", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
    public class CModel
    {
        public CModel() { StringName = ""; ModelName = ""; }
        public string ModelName { get; set; }
        public string StringName { get; set; }
    }

}
