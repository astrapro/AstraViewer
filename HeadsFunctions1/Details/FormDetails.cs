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

namespace HeadsFunctions1.Details
{
    internal partial class FormDetails : Form
    {
        IHeadsApplication hdapp = null;
        bool IsValign = false;
        public FormDetails(IHeadsApplication app, bool bIsValign)
        {
            InitializeComponent();

            this.hdapp = app;
            this.IsValign = bIsValign;

            this.PopulateData();
        }

        private string SelectedModelName
        {
            get
            {
                string ModelName = string.Empty;
                if (this.treeViewDetails.SelectedNode.Tag != null)
                {
                    if (IsValign)
                    {
                        ModelName = ((CValignInfo)this.treeViewDetails.SelectedNode.Tag).ModelName;
                    }
                    else
                    {
                        ModelName = ((CHIPInfo)this.treeViewDetails.SelectedNode.Tag).ModelName;
                    }
                    
                }
                return ModelName;
            }
        }

        private string SelectedStringLabel
        {
            get
            {
                string StringLabel = string.Empty;
                if (this.treeViewDetails.SelectedNode.Tag != null)
                {
                    if (IsValign)
                    {
                        StringLabel = ((CValignInfo)this.treeViewDetails.SelectedNode.Tag).StringLabel;
                    }
                    else
                    {
                        StringLabel = ((CHIPInfo)this.treeViewDetails.SelectedNode.Tag).StringLabel;
                    }
                }
                return StringLabel;
            }
        }

        private TreeNode FindModelNode(string strModelName)
        {
            TreeNode nodemodel = null;
            TreeNode nodeRoot = this.treeViewDetails.Nodes[0];
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

        private void PopulateData()
        {
            TreeNode nodeRoot = this.treeViewDetails.Nodes[0];
            string sFileName = (this.IsValign == false) ? "HALIGN" : "VALIGN";

            string strPath = Path.Combine(this.hdapp.AppDataPath, sFileName +".TMP");
            if (File.Exists(strPath) == false)
            {
                strPath = Path.Combine(this.hdapp.AppDataPath, sFileName+".FIL");
            }

            if (this.IsValign)
            {
                CValignInfo[] infoarr = Valignment.CValignUtil.ReadValigns(strPath);

                foreach (CValignInfo info in infoarr)
                {
                    TreeNode nodemodel = this.FindModelNode(info.ModelName);
                    if (nodemodel == null)
                    {
                        nodemodel = nodeRoot.Nodes.Add(info.ModelName);

                    }

                    TreeNode nodeLabel = nodemodel.Nodes.Add(info.StringLabel);
                    nodeLabel.Tag = info;
                }
            }
            else
            {
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
            }
            
            if (nodeRoot.Nodes.Count > 0)
            {
                nodeRoot.Expand();
            }
        }
        
        private bool ValidateData()
        {
            if (this.tbTextSize_.Value == 0)
            {
                this.errorProvider.SetError(this.tbTextSize_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbTextSize_.Focus();
                return false;
            }

            return true;
        }

        private void treeViewDetails_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewDetails.SelectedNode.Tag != null)
            {
                this.btnOk_.Enabled = true;
            }
            else
            {
                this.btnOk_.Enabled = false;
            }
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            
            if (ValidateData())
            {
                CBox boundbox = this.hdapp.ActiveDocument.BoundingBox;
                if (boundbox != null)
                {
                    //Delete the existing
                    //ViewerUtils.DeleteEntitiesByLabel(this.hdapp.ActiveDocument, HeadsUtils.Constants.LABEL_DETAILS, false);
                    
                    HeadsUtils.Detail Det = new Detail();
                    double z = 0.0;
                    Det.XMin = boundbox.TopLeft.X;
                    Det.YMin = boundbox.TopLeft.Y;
                    z = boundbox.TopLeft.Z;
                    Det.ModelName = this.SelectedModelName;
                    Det.StringName = this.SelectedStringLabel;
                    Det.TextSize = this.tbTextSize_.Value;
                    Det.Rotation = 0;
                    //For Halign Details1 to Details2
                    //For Valign Details2 to Details1
                    //string strFileName = (this.IsValign == false) ? "DETAILS1.TMP" : "DETAILS2.TMP";
                    string PathName = Path.Combine(this.hdapp.AppDataPath, "DETAILS1.TMP");
                    BinaryWriter bw = new BinaryWriter(new FileStream(PathName , FileMode.Create), Encoding.Default);
                    Det.ToStream(bw);
                    bw.Close();

                    string strDetailsOutFile = Path.Combine(this.hdapp.AppDataPath, "DETAILS2.TMP");
                    ViewerUtils.DeleteFileIfExists(strDetailsOutFile);

                    if (this.IsValign == false)
                    {
                        CDetailsHalignUtil util = new CDetailsHalignUtil();
                        util.Funcmain(this.hdapp.AppDataPath);                       
                        
                    }
                    else
                    {
                        CDetailsValignUtil util = new CDetailsValignUtil();
                        util.Funcmain(this.hdapp.AppDataPath, this.hdapp.ActiveDocument.ConfigParam);                        
                    }

                    if (File.Exists(strDetailsOutFile))
                    {
                        DrawingUtil.DrawDrg(this.hdapp, strDetailsOutFile, HeadsUtils.Constants.LABEL_DETAILS, true);
                        this.hdapp.ActiveDocument.RefreshDocument();
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void tbTextSize__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }
    }
}