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
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.Actions;
using VectorDraw.Render;
using System.IO;


namespace HEADSNeed
{
    public partial class frmProperty : Form
    {
        public vdLayout lay;

        public frmProperty()
        {
            InitializeComponent();
            try
            {
                
                //foreach (vdSectionClip sclp in doc.ActiveLayOut.Sections)
                //{
                //    vdBase.ActiveDocument.ActiveLayOut.Sections.AddItem(sclp);
                //}

                //vdBase.ActiveDocument.ActiveLayOut = doc.ActiveLayOut;

                lay = new vdLayout();
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void frmProperty_Load(object sender, EventArgs e)
        {
            MessageBox.Show(lay.ToString());
            try
            {
                //vdBase.ActiveDocument.ActiveLayer = lay.Document.
                
                vdBase.BaseControl.ActiveDocument.ActiveLayOut = lay;
                for (int i = 0; i < lay.Entities.Count; i++)
                {
                    vdBase.BaseControl.ActiveDocument.ActiveLayOut.Entities.AddItem(lay.Entities[i]);
                    
                }

                //vdCircle cir = new vdCircle();
                //cir.SetUnRegisterDocument(vdBase.ActiveDocument);
                //cir.setDocumentDefaults();
                //vdBase.ActiveDocument.ActiveLayOut.Entities.AddItem(cir);
                //cir.Center = new VectorDraw.Geometry.gPoint(11.0d, 12.02d, 13.0d);
                //try
                //{
                //    object ret = vdBase.ActiveDocument.ActionUtility.getUserDist(cir.Center);
                //    cir.Radius = double.Parse(ret.ToString());
                //}
                //catch (Exception exx)
                //{
                //    cir.Radius = 1000.00d;
                //}



                vdBase.BaseControl.ActiveDocument.Redraw(true);
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.ToString());
            }
            propertyGrid1.SelectedObject = vdBase;
            //propertyGrid1.SelectedObject = vdBase.ActiveDocument;
            vdBase.BaseControl.ActiveDocument.Redraw(true);
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vdBase.BaseControl.ActiveDocument.CommandAction.CmdPolyLine("USER");
            
        }

        private void vdBase_MouseClick(object sender, MouseEventArgs e)
        {
            propertyGrid1.SelectedObject = vdBase.BaseControl.ActiveDocument.Selections[0];
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vdBase.BaseControl.ActiveDocument.CommandAction.CmdCircle("USER", null);

        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vdBase.BaseControl.ActiveDocument.CommandAction.CmdLine("USER");

        }

        private void rectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vdBase.BaseControl.ActiveDocument.CommandAction.CmdRect("USER", null);
            //vdBase.BaseControl.ActiveDocument.CommandAction.CmdRotate3d("USER", null, null, null);
            vdBase.BaseControl.ActiveDocument.CommandAction.Pan();
        }
    }

    public class Krishna
    {
        //System.ComponentModel.DisplayNameAttribute
        double x,y,z;
        List<Krishna> listString;
        public Krishna()
        {
            x = 0;
            y = 0;
            z = 0;
            ListString = new List<Krishna>();
        }
        [Category("3D Points")]
        [DisplayName("X")]
        [Description(" This is Calculation of X")]
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        [Category("3D Points")]
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        [Category("3D Points")]
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        public List<Krishna> ListString
        {
            get
            {
                return listString;
            }
            set
            {
                listString = value;
            }
        }
        string sTextString = "";
        
        [Category("Text")]
        //[DisplayName("")]
        [Description("This is simple Text String")]
        public string TextString
        {
            get
            {
                return sTextString;

            }
            set
            {
                sTextString = value;
            }
        }
    }
}
