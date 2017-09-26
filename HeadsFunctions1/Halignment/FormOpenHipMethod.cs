using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.Halignment
{
    internal partial class FormOpenHipMethod : Form,IHalignFilSelector
    {
        public enum OpenMode
        {
            HipMethod = 0,
            ElementMethod,
        }
        CHIPInfo selectedItem;
        IHeadsApplication app;
        OpenMode openmode = OpenMode.ElementMethod;
        public FormOpenHipMethod(IHeadsApplication hdapp, OpenMode mode)
        {
            InitializeComponent();

            this.app = hdapp;
            this.openmode = mode;

            this.PopulateSelectionTree();
        }

        private TreeNode FindModelNode(string strModelName)
        {
            TreeNode nodemodel = null;
            TreeNode nodeRoot = this.treeViewModels_.Nodes[0];
            foreach(TreeNode node in nodeRoot.Nodes)
            {
                if (node.Text == strModelName.Trim())
                {
                    nodemodel = node;
                    break;
                }
            }

            return nodemodel;
        }

        private void PopulateSelectionTree()
        {
            TreeNode nodeRoot = this.treeViewModels_.Nodes[0];
            nodeRoot.Tag = null;
            nodeRoot.Expand();
            string strHalignFilFilePath = System.IO.Path.Combine(this.app.AppDataPath, "HALIGN.FIL");
            CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
            foreach(CHIPInfo info in infoarr)
            {           
                if (openmode == OpenMode.HipMethod)
                {
                    if (info.IsHipData == true)
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
                else if (openmode == OpenMode.ElementMethod)
                {
                    if (info.IsHipData != true)
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
            }
        }

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
            this.selectedItem = (CHIPInfo)this.treeViewModels_.SelectedNode.Tag;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
                

        #region IHalignFilSelector Members
        public Form DialogInstance
        {
            get { return this; }
        }

        public CHIPInfo SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
        }
        #endregion
    }
}