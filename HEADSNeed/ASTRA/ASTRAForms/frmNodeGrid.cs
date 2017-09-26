using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;
namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmNodeGrid : Form
    {
        ASTRADoc astDoc = null;
        vdDocument vdoc;
        int lastId = -1;
        public frmNodeGrid(ASTRADoc astDoc, vdDocument doc)
        {
            InitializeComponent();
            this.astDoc = astDoc;
            vdoc = doc;
            SetGridWithNode();

        }

        private void frmNodeGrid_Load(object sender, EventArgs e)
        {

        }

        public void SetGridWithNode()
        {
            string kStr = "";
            int indx = -1;
            for (int i = 0; i < astDoc.Joints.Count; i++)
            {
                indx = astDoc.Supports.IndexOf(astDoc.Joints[i].NodeNo);
                kStr = "";
                if(indx != -1)
                {
                    kStr = astDoc.Supports[indx].Option.ToString();
                }
                
                dgvNodeGrid.Rows.Add(astDoc.Joints[i].NodeNo,
                    astDoc.Joints[i].Point.x,
                    astDoc.Joints[i].Point.y,
                    astDoc.Joints[i].Point.z,kStr);
                   
            }
        }
        private void dgvNodeGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            gPoint joint = new gPoint();
            joint.x = (double)dgvNodeGrid[1, e.RowIndex].Value;
            joint.y = (double)dgvNodeGrid[2, e.RowIndex].Value;
            joint.z = (double)dgvNodeGrid[3, e.RowIndex].Value;

            VectorDraw.Professional.vdPrimaries.vdFigure fg = null;
            if (lastId != -1)
            {
                for (int i = vdoc.ActiveLayOut.Entities.Count - 1; i >= 0; i--)
                {
                    fg = vdoc.ActiveLayOut.Entities[i];
                    if (fg.Id == lastId)
                    {
                        vdoc.ActiveLayOut.Entities.RemoveAt(i);
                        break;
                    }   
                }
            }
            vdoc.CommandAction.CmdSphere(joint, 0.039, 10, 10);

            lastId = vdoc.ActiveLayOut.Entities.Count - 1;
            fg = vdoc.ActiveLayOut.Entities[lastId];
            lastId = fg.Id;
            fg.PenColor = new vdColor(Color.DarkViolet);

            if ((tssl_zoom.Text == "Zoom On"))
                vdoc.CommandAction.Zoom("W", new gPoint(joint.x + 2.0, joint.y + 2.0, joint.z + 1.0),
                 new gPoint(joint.x - 1.0, joint.y - 2.0, joint.z - 1.0));
            vdoc.Redraw(true);
           
            //if (lastId != -1)
            //{
            //    vdoc.ActiveLayOut.Entities.RemoveAt(lastId);
            //}
            //vdoc.CommandAction.CmdSphere(joint, 0.09, 10, 10);


            //lastId = vdoc.ActiveLayOut.Entities.Count - 1;
            //VectorDraw.Professional.vdPrimaries.vdFigure fg = vdoc.ActiveLayOut.Entities[lastId];
            //fg.PenColor = new vdColor(Color.DarkViolet);

            //if (tsbtn_zoom.Checked)
            //    vdoc.CommandAction.Zoom("W", new gPoint(joint.x + 2.0, joint.y + 2.0, joint.z + 1.0),
            //     new gPoint(joint.x - 1.0, joint.y - 2.0, joint.z - 1.0));
            //vdoc.Redraw(true);

        }

        private void tsbtn_zoom_Click(object sender, EventArgs e)
        {
            //tssl_zoom.Checked = !tssl_zoom.Checked;
            tssl_zoom.Text = (tssl_zoom.Text == "Zoom Off") ? "Zoom On" : "Zoom Off";
            tssl_zoom.BackColor = (tssl_zoom.Text == "Zoom Off") ? Color.Cyan : Color.Red;



        }
        //private void tsbtn_zoom_Click(object sender, EventArgs e)
        //{
        //    tsbtn_zoom.Checked = !tsbtn_zoom.Checked;
        //    tsbtn_zoom.Text = tsbtn_zoom.Checked ? "Zoom On" : "Zoom Off";

        //}
    }
}
