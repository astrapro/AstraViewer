using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;




namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public partial class frmMovingLoadRun : Form
    {
        vdDocument doc;
        ASTRADoc astDoc = null;

        List<int> list_index = null;
        public frmMovingLoadRun(vdDocument vDoc)
        {
            InitializeComponent();
            this.doc = vDoc;

            string fname = @"C:\Documents and Settings\TechSOFT\Desktop\ASTRA Pro Examples\[07] Moving Load Analysis for Bridge Deck\Data Processed\INPUT7.txt";
            astDoc = new ASTRADoc(fname);


            list_index = new List<int>();
        }

        private void frmMovingLoad_Load(object sender, EventArgs e)
        {

        }

        vdLine ln1, ln2, ln3;
        double y = 2d;
        int count = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ln1 = new vdLine();
            ln1 = new vdLine();
            ln1.SetUnRegisterDocument(doc);
            ln1.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(ln1);

            ln1.StartPoint = new gPoint(10d, y, 0d);
            ln1.EndPoint = new gPoint(14d, y, 0d);

            y += 2.0d;

            ln2 = new vdLine();
            ln2 = new vdLine();
            ln2.SetUnRegisterDocument(doc);
            ln2.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(ln2);

            ln2.StartPoint = new gPoint(10d, y, 0d);
            ln2.EndPoint = new gPoint(14d, y, 0d);

            y += 2.0d;

            ln3 = new vdLine();
            ln3 = new vdLine();
            ln3.SetUnRegisterDocument(doc);
            ln3.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(ln3);

            ln3.StartPoint = new gPoint(10d, y, 0d);
            ln3.EndPoint = new gPoint(14d, y, 0d);

            y += 2.0d;
            
            doc.Redraw(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list_index.Clear();
            astDoc.Members.DrawMember(doc);
            DrawASTRAArrowLine();
            doc.Redraw(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Interval;

            for (int i = 0; i < list_index.Count; i++)
            {
                vdFigure al = doc.ActiveLayOut.Entities[list_index[i]];

                if (count == 0)
                    al.visibility = vdFigure.VisibilityEnum.Invisible;
                else
                {
                    al.visibility = vdFigure.VisibilityEnum.Visible;
                    count = -1;
                }
            }
            count++;

            doc.Redraw(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
        public int Interval
        {
            get
            {
                int intv = -1;

                if (!int.TryParse(textBox1.Text, out intv))
                {
                    intv = 1000;
                }
                if (intv <= 0)
                    intv = 200;
                return intv;
            }
        }

        public void DrawASTRAArrowLine()
        {
            ASTRAArrowLine aline = null;
            for (int i = 0; i < astDoc.Joints.Count; i++)
            {
                aline = new ASTRAArrowLine();
                aline.SetUnRegisterDocument(doc);
                aline.setDocumentDefaults();
                doc.ActiveLayOut.Entities.AddItem(aline);
                list_index.Add(doc.ActiveLayOut.Entities.Count - 1);
                //aline.Text
                aline.EndPoint = astDoc.Joints[i].Point;
                aline.StartPoint.x = aline.EndPoint.x;
                aline.StartPoint.y = aline.EndPoint.y + 4.5;
                aline.StartPoint.z = aline.EndPoint.z;
            }

            //doc.Redraw(true);
        }
    }
}
