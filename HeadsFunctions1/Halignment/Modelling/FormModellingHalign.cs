using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1.Halignment;
using System.IO;

namespace HeadsFunctions1.Halignment.Modelling
{
    public partial class FormModellingHalign : Form
    {
        const string ColIndex = "Index#";
        const string ColValus = "Values";
        const double DefaultInterval = 5.0;

        DataTable dtSpChainage = null;
        IHeadsApplication app;
        public FormModellingHalign(IHeadsApplication headsapp)
        {
            InitializeComponent();

            this.app = headsapp;

            this.PopulateSelectionTree();
            this.treeViewDesigns_.SelectedNode = this.treeViewDesigns_.Nodes[0];
        }

        TreeNode FindModelNode(string strModelName)
        {
            TreeNode nodemodel = null;
            TreeNode nodeRoot = this.treeViewDesigns_.Nodes[0];
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
            TreeNode nodeRoot = this.treeViewDesigns_.Nodes[0];
            nodeRoot.Tag = null;
            nodeRoot.Expand();
            string strFilPath = Path.Combine(this.app.AppDataPath, "HALIGN.FIL");
            CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strFilPath);
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
        
        void InitDataGrid()
        {
            this.dtSpChainage = new DataTable("Intervals");
            this.dtSpChainage.Columns.Add(FormModellingHalign.ColIndex, typeof(int));
            this.dtSpChainage.Columns.Add(FormModellingHalign.ColValus, typeof(double));

            this.dataGridSpChainage_.DataSource = this.dtSpChainage;

            this.dataGridSpChainage_.Columns[FormModellingHalign.ColIndex].ReadOnly = true;
            this.dataGridSpChainage_.Columns[FormModellingHalign.ColIndex].DefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridSpChainage_.Columns[FormModellingHalign.ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridSpChainage_.Columns[FormModellingHalign.ColValus].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void treeViewDesigns__AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.tbInterval_.Value = FormModellingHalign.DefaultInterval;
            this.btnDelete_.Enabled = false;

            if (this.treeViewDesigns_.SelectedNode.Tag != null)
            {
                this.InitDataGrid();
                this.tbInterval_.Enabled = true;
                this.btnOk_.Enabled = true;
                this.btnInsert_.Enabled = true;                             
            }
            else
            {
                if (this.dtSpChainage != null)
                {
                    this.dtSpChainage.Clear();
                }
                
                this.dataGridSpChainage_.DataSource = null;
                this.tbInterval_.Enabled = false;
                this.btnOk_.Enabled = false;
                this.btnInsert_.Enabled = false;   
            }
        }

        private void btnInsert__Click(object sender, EventArgs e)
        {
            if (this.dataGridSpChainage_.SelectedRows.Count > 0)
            {
                int iIndex = this.dataGridSpChainage_.SelectedRows[0].Index;

                DataRow row = dtSpChainage.NewRow();
                row[ColIndex] = 0;
                row[ColValus] = 0;

                this.dtSpChainage.Rows.InsertAt(row, iIndex);

                for(int i = 0; i < dtSpChainage.Rows.Count; i++)
                {
                    dtSpChainage.Rows[i][ColIndex] = i + 1;
                }
                
            }
            else
            {
                int rows = dtSpChainage.Rows.Count;

                DataRow row = dtSpChainage.NewRow();
                row[ColIndex] = rows + 1;
                row[ColValus] = 0;

                if (rows > 0)
                {
                    row[ColValus] = ((double)dtSpChainage.Rows[rows-1][ColValus]) + 1;
                }

                dtSpChainage.Rows.Add(row);
            }
            
        }

        private void btnDelete__Click(object sender, EventArgs e)
        {
            if (this.dataGridSpChainage_.SelectedRows.Count == 1)
            {
                int iIndex = this.dataGridSpChainage_.SelectedRows[0].Index;
                this.dtSpChainage.Rows.RemoveAt(iIndex);
                for (int i = 0; i < dtSpChainage.Rows.Count; i++)
                {
                    dtSpChainage.Rows[i][ColIndex] = i + 1;
                }
            }
        }

        private void dataGridSpChainage__SelectionChanged(object sender, EventArgs e)
        {
            this.btnDelete_.Enabled = (this.dataGridSpChainage_.SelectedRows.Count > 0) ? true : false;
            
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridSpChainage__DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridSpChainage_.CancelEdit();
        }

        private void treeViewDesigns__NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GenerateModel();
        }

        void GenerateModel()
        {
            if (this.treeViewDesigns_.SelectedNode != null && this.treeViewDesigns_.SelectedNode.Tag != null)
            {
                CHIPInfo info = (CHIPInfo)this.treeViewDesigns_.SelectedNode.Tag;
                HalMod1type data = new HalMod1type();
                data.ModelName = info.ModelName;
                data.StringName = info.StringLabel;
                data.inc = this.tbInterval_.Value;
                data.TotalSpChn = dtSpChainage.Rows.Count;

                string strPath = Path.Combine(this.app.AppDataPath, "HALMOD1.TMP");
                BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Create), Encoding.Default);
                data.ToStream(bw);

                if (this.dtSpChainage.Rows.Count > 0)
                {
                    StreamWriter sw = new StreamWriter(Path.Combine(this.app.AppDataPath, "SPCHN.TMP"));
                    for (int i = 0; i < this.dtSpChainage.Rows.Count; i++)
                    {
                        double dVal = (double)this.dtSpChainage.Rows[i][ColValus];
                        bw.Write(dVal);
                        sw.WriteLine(data.ModelName + "\t" + data.StringName  + "\t" + dVal.ToString());
                    }
                    sw.Close();
                }
                
                bw.Close();

                FormHalignModelViewer modelviewer = new FormHalignModelViewer(this.app);
                modelviewer.ShowDialog();

            }            
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            GenerateModel();
            this.Close();
        }

        private void dataGridSpChainage__CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridSpChainage_.Columns[FormModellingHalign.ColValus].Index)
            {
                if (e.RowIndex > 0)
                {
                    double dValCur = (double)this.dtSpChainage.Rows[e.RowIndex][FormModellingHalign.ColValus];
                    double dValPrev = (double)this.dtSpChainage.Rows[e.RowIndex - 1][FormModellingHalign.ColValus];
                    if (dValCur <= dValPrev)
                    {
                        MessageBox.Show(this, "Chainage not in proper sequence", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridSpChainage_.CancelEdit();
                    }
                }
            }
        }
    }
}