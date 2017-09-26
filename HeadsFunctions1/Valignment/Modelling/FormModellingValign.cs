using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.IO;

namespace HeadsFunctions1.Valignment.Modelling
{
    public partial class FormModellingValign : Form
    {
        CValignInfo selectedItem;
        IHeadsApplication app;
        public FormModellingValign(IHeadsApplication app)
        {
            this.app = app;
            InitializeComponent();
            this.btnOK_.Enabled = false;
            PopulateTree();
        }

        private void btnOK__Click(object sender, EventArgs e)
        {
            double chainageVal = textChainInt_.Value;

            this.selectedItem = (CValignInfo)this.treeViewModels_.SelectedNode.Tag;
            if (selectedItem != null)
            {
                FormModellingValignViewer codeForm = new FormModellingValignViewer(this.selectedItem ,chainageVal, app);
                codeForm.Owner = this;
                codeForm.ShowDialog();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void PopulateTree()
        {
            this.treeViewModels_.Nodes.Add("ValignFil");
            TreeNode nodeRoot = this.treeViewModels_.Nodes[0];
            nodeRoot.Tag = null;
            nodeRoot.Expand();
            string strValignFilPath = Path.Combine(this.app.AppDataPath, "VALIGN.FIL");
            CValignInfo[] infoarr = CValignUtil.ReadValigns(strValignFilPath);
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

        private TreeNode FindModelNode(string strModelName)
        {
            TreeNode nodemodel = null;
            TreeNode nodeRoot = this.treeViewModels_.Nodes[0];
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

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void treeViewModels__AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.selectedItem = (CValignInfo)this.treeViewModels_.SelectedNode.Tag;
            this.btnOK_.Enabled = (selectedItem != null);
        }
        
    }
}