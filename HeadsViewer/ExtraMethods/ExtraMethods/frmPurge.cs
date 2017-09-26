//
// (C) Copyright 1998-2007 by VectorDraw, Ltd. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// VectorDraw PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// VectorDraw SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  VectorDraw, Ltd. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;

namespace ExtraMethods
{
    public partial class frmPurge : Form
    {
        private vdDocument mDoc =null;
        private vdLayers layers = new vdLayers ();
        private vdBlocks blocks = new vdBlocks ();
        private vdDimstyles dimstyles = new vdDimstyles ();
        private vdLineTypes linetypes = new vdLineTypes();
        private vdTextstyles textstyles = new vdTextstyles();
        private vdImages images = new vdImages();
        private vdHatchPatterns hatchpatterns = new vdHatchPatterns();
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public frmPurge()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is called before the dialog is shown.
        /// </summary>
        /// <param name="doc"></param>
        public void InitializeDialog(vdDocument doc)
        {
            mDoc = doc;
            //This method fills the passed parameter collections with all the unused items of the document.
            mDoc.GetUnUsedTables(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns);
        }
        /// <summary>
        /// Fills the tree view with the items to be purged of the passed Document.
        /// </summary>
        private void FillTreeView()
        {
            int items = 0;
            int count = 0;

            tree.Nodes.Clear();
            tree.BeginUpdate();
            tree.FullRowSelect = false;
            TreeNode All = new TreeNode("All Items");
            
            //Layers
            TreeNode NodeLayers = new TreeNode();
            foreach (vdLayer lay in layers)
            {
                if (lay.Deleted == false)
                {
                    NodeLayers.Nodes.Add(lay.Name);
                    items++;
                    count++;
                }
            }
            NodeLayers.Text = "Layers(" + count.ToString() + " items)";
            All.Nodes.Add(NodeLayers);

            count = 0;
            //Blocks
            TreeNode NodeBlocks = new TreeNode("Blocks(" + blocks.Count.ToString()+" items)");
            foreach (vdBlock blk in blocks)
            {
                if (blk.Deleted == false)
                {
                    NodeBlocks.Nodes.Add(blk.Name);
                    items++;
                    count++;
                }
            }
            NodeBlocks.Text = "Blocks(" + count.ToString()+" items)";
            All.Nodes.Add(NodeBlocks);

            count = 0;
            //Dimstyles
            TreeNode NodeDimstyles = new TreeNode();
            foreach (vdDimstyle dim in dimstyles)
            {
                if (dim.Deleted == false)
                {
                    NodeDimstyles.Nodes.Add(dim.Name);
                    items++;
                    count++;
                }
            }
            NodeDimstyles.Text = "DimStyles(" + count.ToString() + " items)";
            All.Nodes.Add(NodeDimstyles);

            count = 0;
            //Linetypes
            TreeNode NodeLinetypes = new TreeNode();
            foreach (vdLineType linetype in linetypes)
            {
                if (linetype.Deleted == false)
                {
                    NodeLinetypes.Nodes.Add(linetype.Name);
                    items++;
                    count++;
                }
            }
            NodeLinetypes.Text = "LineTypes(" + count.ToString() + " items)";
            All.Nodes.Add(NodeLinetypes);

            count = 0;
            //TextStyles
            TreeNode NodeTextStyles = new TreeNode();
            foreach (vdTextstyle txtstyle in textstyles)
            {
                if (txtstyle.Deleted == false)
                {
                    NodeTextStyles.Nodes.Add(txtstyle.Name);
                    items++;
                    count++;
                }
            }
            NodeTextStyles.Text = "TextStyles(" + count.ToString() + " items)";
            All.Nodes.Add(NodeTextStyles);

            count = 0;
            //Images
            TreeNode NodeImages = new TreeNode();
            foreach (vdImageDef def in images)
            {
                if (def.Deleted == false)
                {
                    NodeImages.Nodes.Add(def.Name);
                    items++;
                    count++;
                }
            }
            NodeImages.Text = "Images(" + count.ToString() + " items)";
            All.Nodes.Add(NodeImages);

            count = 0;
            //HatchPatterns
            TreeNode NodeHatches = new TreeNode();
            foreach (vdHatchPattern pat in hatchpatterns)
            {
                if (pat.Deleted == false)
                {
                    NodeHatches.Nodes.Add(pat.Name);
                    items++;
                    count++;
                }
            }
            NodeHatches.Text = "HatchPatterns(" + count.ToString() + " items)";
            All.Nodes.Add(NodeHatches);

            All.Expand();
            tree.Nodes.Add(All);
            
            tree.EndUpdate();
            tree.Refresh();

            label1.Text = "Items Not Used in Document (" + items.ToString() + " items)";

        }
        /// <summary>
        /// Loads the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurge_Load(object sender, EventArgs e)
        {
            FillTreeView();
        }

        #region Buttons
        /// <summary>
        /// Enables all checkboxes to be purged(of all collections).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAll_Click(object sender, EventArgs e)
        {
            checkBlocks.Checked = true;
            checkDimstyles.Checked = true;
            checkLayers.Checked = true;
            checkImages.Checked = true;
            checkTextStyles.Checked = true;
            checkLineTypes.Checked = true;
            checkHatchPatterns.Checked = true;
        }
        /// <summary>
        /// Cancels and closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Disables all checkboxes to be purged(of all collections).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            checkBlocks.Checked = false;
            checkDimstyles.Checked = false;
            checkLayers.Checked = false;
            checkImages.Checked = false;
            checkTextStyles.Checked = false;
            checkLineTypes.Checked = false;
            checkHatchPatterns.Checked = false;
        }
       
        #endregion
        /// <summary>
        /// Purges all the selected collections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butPurge_Click(object sender, EventArgs e)
        {
            int items = 0;
            if (checkBlocks.Checked) items += mDoc.Blocks.Purge();
            if (checkDimstyles.Checked) items += mDoc.DimStyles.Purge();
            if (checkLayers.Checked) items += mDoc.Layers.Purge();
            if (checkImages.Checked) items += mDoc.Images.Purge();
            if (checkTextStyles.Checked) items += mDoc.TextStyles.Purge();
            if (checkLineTypes.Checked) items += mDoc.LineTypes.Purge();
            if (checkHatchPatterns.Checked) items += mDoc.HatchPatterns.Purge();

            mDoc.GetUnUsedTables(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns);
            FillTreeView();
            MessageBox.Show(this,items.ToString() + " items purged");
        }

       
    }
}