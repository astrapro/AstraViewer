using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.Chainage
{
    internal partial class FormChainage : Form
    {
        IHeadsApplication hdapp;
        public FormChainage(IHeadsApplication app)
        {
            InitializeComponent();
            this.hdapp = app;

            PopulateData();
        }
        
        private string SelectedModelName
        {
            get
            {
                string ModelName = string.Empty;
                if (this.treeViewChainage.SelectedNode.Tag != null)
                {
                    ModelName = ((CHIPInfo)this.treeViewChainage.SelectedNode.Tag).ModelName;
                }
                return ModelName;
            }
        }

        private string SelectedStringLabel
        {
            get
            {
                string StringLabel = string.Empty;
                if (this.treeViewChainage.SelectedNode.Tag != null)
                {
                    StringLabel = ((CHIPInfo)this.treeViewChainage.SelectedNode.Tag).StringLabel;
                }
                return StringLabel;
            }
        }

        private void PopulateData()
        {
            TreeNode nodeRoot = this.treeViewChainage.Nodes[0];
            string strPath = Path.Combine(this.hdapp.AppDataPath, "HALIGN.TMP");
            if (File.Exists(strPath) == false)
            {
                strPath = Path.Combine(this.hdapp.AppDataPath, "HALIGN.FIL");
            }

            CHIPInfo[] infoarr = Halignment.CHalignHipUtil.ReadHaligns(strPath);

            foreach (CHIPInfo info in infoarr)
            {
                TreeNode nodemodel = this.FindModelNode(info.ModelName);
                if (nodemodel == null)
                {
                    nodemodel = nodeRoot.Nodes.Add(info.ModelName);
                    
                }

                TreeNode nodeLabel = nodemodel.Nodes.Add(info.StringLabel);
                nodeLabel.Tag = info;
            }
            if (nodeRoot.Nodes.Count > 0)
            {
                nodeRoot.Expand();
            }
            
        }

        private TreeNode FindModelNode(string strModelName)
        {
            TreeNode nodemodel = null;
            TreeNode nodeRoot = this.treeViewChainage.Nodes[0];
            foreach (TreeNode node in nodeRoot.Nodes)
            {
                if (node.Text == strModelName.Trim())
                {
                    nodemodel = node;
                    break;
                }
            }
            return nodemodel;
        }

        private bool ValidateData()
        {
            if (this.tbChainageInterval_.Value == 0)
            {
                this.errorProvider.SetError(this.tbChainageInterval_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbChainageInterval_.Focus();
                return false;
            }
            if (this.tbTextSize_.Value == 0)
            {
                this.errorProvider.SetError(this.tbTextSize_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbTextSize_.Focus();
                return false;
            }

            return true;
        }

        private void treeViewChainage_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewChainage.SelectedNode.Tag != null)
            {
                this.btnOk_.Enabled = true;
            }
            else
            {
                this.btnOk_.Enabled = false;
            }
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                CBox boundbox = this.hdapp.ActiveDocument.BoundingBox;
                if (boundbox != null)
                {
                    //Delete the existing
                    //ViewerUtils.DeleteEntitiesByLabel(this.hdapp.ActiveDocument, HeadsUtils.Constants.LABEL_CHAINAGE, false);

                    //CCfgtype cfg = this.hdapp.ConfigParam;
                    HeadsUtils.Chainage Chng = new HeadsUtils.Chainage();
                    double z = 0.0;
                    Chng.XMin = boundbox.TopLeft.X;
                    Chng.YMin = boundbox.TopLeft.Y;
                    z = boundbox.TopLeft.Z;
                    Chng.ModelName = this.SelectedModelName;
                    Chng.StringName = this.SelectedStringLabel;
                    Chng.TextSize = this.tbTextSize_.Value;
                    Chng.ChainageInterval = this.tbChainageInterval_.Value;
                    Chng.Rotation = 0;

                    BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(this.hdapp.AppDataPath, "CHAIN1.TMP"), FileMode.Create), Encoding.Default);
                    bw.Write(ViewerUtils.ConvertStringToByteArray(Chng.ModelName, 30));
                    bw.Write(ViewerUtils.ConvertStringToByteArray(Chng.StringName, 20));
                    bw.Write(Chng.XMin);
                    bw.Write(Chng.YMin);
                    bw.Write(Chng.ChainageInterval);
                    bw.Write(Chng.TextSize);
                    bw.Write(Chng.Rotation);
                    bw.Close();

                    ViewerUtils.DeleteFileIfExists(Path.Combine(this.hdapp.AppDataPath, "CHAIN2.TMP"));

                    CChainageUtil util = new CChainageUtil();
                    util.Funcmain(this.hdapp.AppDataPath);

                    DrawingUtil.DrawChainage(this.hdapp, Path.Combine(this.hdapp.AppDataPath, "CHAIN2.TMP"), "CHAIN", true);

                    this.hdapp.ActiveDocument.RefreshDocument();
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }           
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbChainageInterval__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }

        private void tbTextSize__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }        
    }
}