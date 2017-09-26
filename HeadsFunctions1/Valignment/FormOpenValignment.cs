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

namespace HeadsFunctions1.Valignment
{
    internal partial class FormOpenValignment : Form, IValignFilSelector
    {
        IHeadsApplication app;
        CValignInfo selectedItem;
        public FormOpenValignment(IHeadsApplication hdapp)
        {
            InitializeComponent();

            this.app = hdapp;

            this.PopulateSelectionTree();
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

        void PopulateSelectionTree()
        {
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

        #region IValignFilSelector Members

        public Form DialogInstance
        {
            get { return this; }
        }

        public CValignInfo SelectedItem
        {
            get { return selectedItem; }
        }

        #endregion

        private void treeViewModels__AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeViewModels_.SelectedNode.Tag != null)
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
            this.selectedItem = (CValignInfo)this.treeViewModels_.SelectedNode.Tag;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}