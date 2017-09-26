using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;
using HEADSNeed.ASTRA.ASTRAClasses;
using HeadsUtils;

namespace HEADSNeed.DrawingToData
{
    public partial class frmDrawPolylineFromFile : Form
    {
        vdDocument vDoc = null;
        ColumnIndexes indx = null;
        DrawPolyLineCollection dpl_col = null;
        bool isDataToPoly = false;
        bool isModeling = false;

        #region Form Event
        public frmDrawPolylineFromFile(vdDocument vddoc, bool _IsDataToPolyLine)
        {
            InitializeComponent();
            vDoc = vddoc;
            indx = new ColumnIndexes();
            isDataToPoly = _IsDataToPolyLine;
            if (!isDataToPoly)
            {
                DrawingToData(vddoc);
                SetLayerLabel();
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fbd = new OpenFileDialog())
            {
                fbd.Filter = "Space(' ') Separated Files [*.txt]|*.txt|Comma(',') Separated File [*.csv]|*.csv|All Files[*.*]|*.*";
                if (fbd.ShowDialog() != DialogResult.Cancel)
                {
                    txt_select_file.Text = fbd.FileName;
                    FillGrid();
                }
            }
        }
        private void btn_Proceed_Click(object sender, EventArgs e)
        {
            try
            {
                if (isDataToPoly)
                {
                    //dpl_col = new DrawPolyLineCollection(txt_select_file.Text, GetCurrentIndexes);
                    ////DataToPolyLine();
                    //frmOptions fo = new frmOptions(vDoc);
                    //fo.Owner = this.Owner;
                    //fo.Show();
                    //this.Close();
                }
                else
                {
                    SetLabels();
                    if (IsModeling)
                    {
                        string ff = Path.Combine(WorkingFolder, "survey.tmp");
                        WriteData(ff);
                        WriteModel(ff);
                    }
                    else
                    {
                        if (sfd.ShowDialog() != DialogResult.Cancel)
                        {
                            WriteData(sfd.FileName);
                        }
                    }
                    MessageBox.Show("WriteData completed.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Or Wrong data file.", "HEADS Viewer");
            }
        }

        private void btn_Finish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void frmDrawPolylineFromFile_Load(object sender, EventArgs e)
        {
            if (isDataToPoly)
            {
                pnl_buttons.Location = new Point(221, 327);
                this.Size = new Size(686, 405);
            }
            else
            {
                pnl_buttons.Location = new Point(12, 297);
                this.Size = new Size(281, 377);
            }

            grb_Poly_To_Text.Visible = !isDataToPoly;
            //dataGridView1.Visible = isDataToPoly;
            //dataGridView2.Visible = isDataToPoly;
            grb_data_to_poly.Visible = isDataToPoly;

            this.Text = (isDataToPoly) ? "Drawing Data file to Polyline" : "Polyline to Data File";
            btn_Proceed.Text = (isDataToPoly) ? "Next" : "Save As";

            if (IsModeling)
            {
                btn_Proceed.Text = "Write Model";
            }
        }
        private void txt_select_file_TextChanged(object sender, EventArgs e)
        {
            if (isDataToPoly)
            {
                btn_Proceed.Enabled = File.Exists(txt_select_file.Text);
            }
        }

        #endregion

        #region Form Properties
        private ColumnIndexes GetCurrentIndexes
        {
            get
            {
                indx = new ColumnIndexes();
                //SetIndex(cmb_col1.SelectedIndex, 0);
                //SetIndex(cmb_col2.SelectedIndex, 1);
                //SetIndex(cmb_col3.SelectedIndex, 2);
                //SetIndex(cmb_col4.SelectedIndex, 3);
                //SetIndex(cmb_col5.SelectedIndex, 4);
                //SetIndex(cmb_col6.SelectedIndex, 5);
                //SetIndex(cmb_col7.SelectedIndex, 6);
                //SetIndex(cmb_col8.SelectedIndex, 7);
                //SetIndex(cmb_col9.SelectedIndex, 8);
                //SetIndex(cmb_col10.SelectedIndex, 9);
                try
                {
                    SetIndex(dataGridView1[0, 0].Value.ToString(), 0);
                    SetIndex(dataGridView1[1, 0].Value.ToString(), 1);
                    SetIndex(dataGridView1[2, 0].Value.ToString(), 2);
                    SetIndex(dataGridView1[3, 0].Value.ToString(), 3);
                    SetIndex(dataGridView1[4, 0].Value.ToString(), 4);
                    //Chiranjit 2010/11/29
                    SetIndex(dataGridView1[5, 0].Value.ToString(), 5);
                    SetIndex(dataGridView1[6, 0].Value.ToString(), 6);
                    SetIndex(dataGridView1[7, 0].Value.ToString(), 7);
                    SetIndex(dataGridView1[8, 0].Value.ToString(), 8);
                    SetIndex(dataGridView1[9, 0].Value.ToString(), 9);

                    dataGridView1.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    //throw new Exception("Wrong Column Input index");
                }
                //SetIndex(dataGridView1[5, 0].Value.ToString(), 5);
                //SetIndex(dataGridView1[6, 0].Value.ToString(), 6);
                //SetIndex(dataGridView1[7, 0].Value.ToString(), 7);
                //SetIndex(dataGridView1[8, 0].Value.ToString(), 8);
                //SetIndex(dataGridView1[9, 0].Value.ToString(), 9);


                //MessageBox.Show(dataGridView1[0, 0].Value.ToString());


                return indx;
            }
        }
        public DrawPolyLineCollection PolyLineCollection
        {
            get
            {
                return dpl_col;
            }
        }
        public bool IsDataToPolyLine
        {
            get
            {
                return isDataToPoly;
            }
        }
        public bool IsModeling { get; set; }
        public string WorkingFolder { get; set; }
        #endregion

        #region Polyline To Data
        public void DrawingToData1(vdDocument vdoc)
        {
            MessageBox.Show("Text Data file will be created from selected Survey Base Plan.", "HEADS", MessageBoxButtons.OK);
            vdFigure vFig;
            gPoint gp = new gPoint();

            vdoc.Prompt("Select Entities:");

            //vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            vdSelection vsel = vdoc.ActionUtility.getUserSelection();
            IvdPolyline pLine = null;
            IvdText vdTxt = null;
            dpl_col = new DrawPolyLineCollection();
            DrawPolyLine dpl = null;
            int pt_no = 0;
            string last_label = "";
            
        }
        public void DrawingToData(vdDocument vdoc)
        {
            MessageBox.Show("Text Data file will be created from selected Survey Base Plan.", "HEADS", MessageBoxButtons.OK);
            vdFigure vFig;
            gPoint gp = new gPoint();

            vdoc.Prompt("Select Entities:");

            //vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            vdSelection vsel = vdoc.ActionUtility.getUserSelection();
            IvdPolyline pLine = null;
            IvdText vdTxt = null;
            dpl_col = new DrawPolyLineCollection();
            DrawPolyLine dpl = null;
            int pt_no = 0;
            string last_label = "";


            for (int sel = 0; sel < vsel.Count; sel++)
            {
                vFig = vsel[sel];
                //pLine = vFig as vdPolyline;
                double last_chainage = 0.0;

                //if (pLine is vdPolyline)
                if (vFig is vdPolyline)
                {
                    #region Polyline
                    pLine = vFig as vdPolyline;
                    last_chainage = 0.0;
                    for (int i = 0; i < pLine.VertexList.Count; i++)
                    {
                        try
                        {
                            dpl = new DrawPolyLine();
                            //dpl.PointNo = i + 1;
                            if (last_label != pLine.Layer.Name)
                            {
                                pt_no = 0;
                            }
                            pt_no++;
                            dpl.PointNo = pt_no;
                            if (i > 0)
                            {
                                dpl.SegmentLength = pLine.VertexList[i - 1].Distance2D(pLine.VertexList[i]);
                                last_chainage += dpl.SegmentLength;
                            }
                            else
                            {
                                dpl.SegmentLength = 0.0;
                            }
                            //dpl.LayerName = txt_layer_name.Text;
                            //dpl.Label = txt_label_name.Text;
                            last_label = pLine.Layer.Name;
                            dpl.LayerName = pLine.Layer.Name;
                            dpl.Label = pLine.Layer.Name;
                            dpl.Chainge = last_chainage;
                            dpl.X = pLine.VertexList[i].x;
                            dpl.Y = pLine.VertexList[i].y;
                            dpl.Z = (pLine.VertexList[i].z == 0.0) ? -999.0 : pLine.VertexList[i].z;
                            dpl.DrawFormat = DrawAs.Polyline;
                            dpl_col.Add(dpl);
                        }
                        catch (Exception ex) { }
                    }
                    if (pLine.Flag == VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE)
                    {
                        dpl = new DrawPolyLine();
                        dpl.PointNo = ++pt_no;
                        dpl.LayerName = pLine.Layer.Name;
                        dpl.Label = pLine.Layer.Name;
                        dpl.Chainge = last_chainage;
                        dpl.X = pLine.VertexList[0].x;
                        dpl.Y = pLine.VertexList[0].y;
                        dpl.Z = (pLine.VertexList[0].z == 0.0) ? -999.0 : pLine.VertexList[0].z;
                        dpl.DrawFormat = DrawAs.Polyline;
                        dpl_col.Add(dpl);
                    }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = pLine.Layer.Name;
                    dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dpl_col[dpl_col.Count - 1].Z;
                    dpl_col.Add(dpl);

                #endregion
                }
                else if (vFig is vdText)
                {
                    #region Text
                    vdTxt = vFig as vdText;
                    last_chainage = 0.0;
                    /**/

                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdTxt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdTxt.TextString;
                        last_label = vdTxt.Layer.Name;
                        dpl.LayerName = vdTxt.Layer.Name;
                        //dpl.Label = pLine.Layer.Name;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdTxt.InsertionPoint.x;
                        dpl.Y = vdTxt.InsertionPoint.y;
                        dpl.Z = (vdTxt.InsertionPoint.z == 0.0) ? -999.0 : vdTxt.InsertionPoint.z;

                        dpl.TextString = vdTxt.TextString;
                        dpl.TextHeight = vdTxt.Height;
                        dpl.TextRotation = vdTxt.Rotation;
                        dpl.DrawFormat = DrawAs.Text;
                        dpl_col.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdTxt.Layer.Name;
                    dpl.Label = vdTxt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dpl_col[dpl_col.Count - 1].Z;
                    dpl_col.Add(dpl);

                    #endregion
                }
                else if (vFig is vdPoint)
                {
                    #region Point
                    vdPoint vdpnt = vFig as vdPoint;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdpnt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdpnt.Layer.Name;
                        dpl.LayerName = vdpnt.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.InsertionPoint.x;
                        dpl.Y = vdpnt.InsertionPoint.y;
                        dpl.Z = (vdpnt.InsertionPoint.z == 0.0) ? -999.0 : vdpnt.InsertionPoint.z;
                        dpl.DrawFormat = DrawAs.Point;
                        dpl_col.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdpnt.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dpl_col[dpl_col.Count - 1].Z;
                    dpl_col.Add(dpl);

                    #endregion
                }
                else if (vFig is vdInsert)
                {
                    #region Insert
                    vdInsert vdins = vFig as vdInsert;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdins.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdins.Layer.Name;
                        dpl.LayerName = vdins.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdins.InsertionPoint.x;
                        dpl.Y = vdins.InsertionPoint.y;
                        dpl.Z = (vdins.InsertionPoint.z == 0.0) ? -999.0 : vdins.InsertionPoint.z;
                        dpl.DrawFormat = DrawAs.FromFile;
                        dpl_col.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdins.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dpl_col[dpl_col.Count - 1].Z;
                    dpl_col.Add(dpl);

                    #endregion
                }
                else if (vFig is vdLine)
                {
                    #region Line
                    vdLine vdpnt = vFig as vdLine;
                    last_chainage = 0.0;
                    /**/
                    try
                    {
                        dpl = new DrawPolyLine();
                        //dpl.PointNo = i + 1;
                        if (last_label != vdpnt.Layer.Name)
                        {
                            pt_no = 0;
                        }
                        pt_no++;
                        dpl.PointNo = pt_no;

                        //dpl.LayerName = txt_layer_name.Text;
                        //dpl.Label = vdpnt.TextString;
                        last_label = vdpnt.Layer.Name;
                        dpl.LayerName = vdpnt.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.StartPoint.x;
                        dpl.Y = vdpnt.StartPoint.y;
                        dpl.Z = (vdpnt.StartPoint.z == 0.0) ? -999.0 : vdpnt.StartPoint.z;
                        dpl.DrawFormat = DrawAs.Line;
                        dpl_col.Add(dpl);

                        dpl = new DrawPolyLine();
                        dpl.LayerName = vdpnt.Layer.Name;
                        //dpl.Label = txt_label_name.Text;
                        dpl.Chainge = last_chainage;
                        dpl.X = vdpnt.EndPoint.x;
                        dpl.Y = vdpnt.EndPoint.y;
                        dpl.Z = (vdpnt.EndPoint.z == 0.0) ? -999.0 : vdpnt.EndPoint.z;
                        dpl.DrawFormat = DrawAs.Line;
                        dpl_col.Add(dpl);
                    }
                    catch (Exception ex) { }
                    dpl = new DrawPolyLine();
                    dpl.LayerName = vdpnt.Layer.Name;
                    //dpl.Label = txt_label_name.Text;
                    //dpl.Label = vdpnt.TextString;
                    //dpl.Label = pLine.Layer.Name;
                    dpl.Chainge = last_chainage;
                    dpl.X = 0.0;
                    dpl.Y = 0.0;
                    dpl.Z = dpl_col[dpl_col.Count - 1].Z;
                    dpl_col.Add(dpl);

                    #endregion
                }
            }
        }
        public void SetLayerLabel()
        {
            List<string> list_layer = new List<string>();
            dgv_layer_label.Rows.Clear();
            for (int i = 0; i < dpl_col.Count; i++)
            {
                if (!list_layer.Contains(dpl_col[i].LayerName))
                {
                    list_layer.Add(dpl_col[i].LayerName);
                    dgv_layer_label.Rows.Add(dpl_col[i].LayerName, "");
                }
            }
            //for (int i = 0; i < dpl_col.Labels.Count; i++)
            //{
            //        //list_layer.Add(dpl_col.Labels[i]);
            //        dgv_layer_label.Rows.Add(dpl_col.Labels[i], "");
            //}
            list_layer.Clear();
            list_layer = null;
        }
        void SetLabels()
        {
            Hashtable ht = new Hashtable();
            string layer, label;
            layer = string.Empty;
            label = string.Empty;

            int i = 0;
            for (i = 0; i < dgv_layer_label.Rows.Count; i++)
            {

                layer = dgv_layer_label[0, i].Value.ToString();
                label = dgv_layer_label[1, i].Value.ToString();
                if (label != null && layer != null)
                {
                    ht.Add(layer, label);
                }
            }
            for (i = 0; i < dpl_col.Count; i++)
            {
                layer = dpl_col[i].LayerName;
                label =(string) ht[layer];
                if (label != null)
                {
                    dpl_col[i].Label = (label == "") ? layer : label;
                }

            }
            ht.Clear();
            ht = null;
        }


        public void WriteData(string file_name)
        {

            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            string text_file = Path.Combine(Path.GetDirectoryName(file_name), "Text_Data.txt");
            StreamWriter sw2 = new StreamWriter(new FileStream(text_file, FileMode.Create));

            string sp_char = ((Path.GetExtension(file_name).ToLower() == ".csv")) ? "," : " ";
            try
            {
                //sw.WriteLine("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}{9}",
                //        "Point Nos.", sp_char,
                //        "SegmentLength",
                //        "CHAINGE",
                //        "X",
                //        "Y",
                //        "Z",
                //        "LAYER",
                //        "LABEL",
                //        "HEIGHT"); 



                //sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                //   "",
                //   "",
                //   "",
                //   "",
                //   "",
                //   "");
                sw.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "Serial", sp_char,
                   "Easting",
                   "Northing",
                   "Elevation",
                   "Feature");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "No:", sp_char,
                   "  (Metres)",
                   "  (Metres)",
                   "  (Metres)",
                   "Code");
                sw2.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                "Serial", sp_char,
                "Easting",
                "Northing",
                "Elevation",
                "Feature");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);

                sw2.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "No:", sp_char,
                   "(Metres)",
                   "(Metres)",
                   "(Metres)",
                   "Code");

                //sw.WriteLine("No:{0}   (Mtres){0}  (Metres){0} (Metre){0}   Code", sp_char);
                //sw2.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                //sw2.WriteLine("No:{0}   (Mtres){0}  (Metres){0} (Metre){0}   Code", sp_char);

                for (int i = 0; i < dpl_col.Count; i++)
                {
                    if (dpl_col[i].DrawFormat == DrawAs.Text)
                    {
                        if (dpl_col[i].PointNo == 0)
                        {
                            //sw.WriteLine("{0}   {1}  {2:f3} {1} {3:f3}  {1} {4:f3}{1}  {5}",
                            sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName);
                                    //,dpl_col[i].Label
                        }
                        else
                        {
                            sw2.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}   {6} {7} {8}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName,
                                    dpl_col[i].TextHeight,
                                    dpl_col[i].TextRotation,
                                    dpl_col[i].TextString);

                            sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    "TEXT");
                        }
                    }
                    else
                    {
                        if (dpl_col[i].PointNo == 0)
                        {
                            sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                            //sw.WriteLine("{0}   {1}  {2:f3} {1} {3:f3}  {1} {4:f3}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName);
                        }
                        else
                        {
                            //sw.WriteLine("{0}{1}{2:f5}{1}{3:f5}{1}{4:f5}{1}  {5} ",
                            sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                                    dpl_col[i].PointNo, sp_char,
                                    dpl_col[i].X,
                                    dpl_col[i].Y,
                                    dpl_col[i].Z,
                                    dpl_col[i].LayerName);
                        }
                    }
                    //sw.WriteLine("{0}{1}{2:f4}{1}{3:f4}{1}{4:f4}{1}{5:f4}{1}{6:f4}{1}{7}{1}{8}{1}{9:f4}",
                    //    (i + 1), sp_char,
                    //    dpl_col[i].SegmentLength,
                    //    dpl_col[i].Chainge,
                    //    dpl_col[i].X,
                    //    dpl_col[i].Y,
                    //    dpl_col[i].Z,
                    //    dpl_col[i].LayerName,
                    //    dpl_col[i].Label,
                    //    txt_height.Text);
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sw2.Flush();
                sw2.Close();
            }
        }
        public void WriteModel(string file_name)
        {
            //string file_name
            //string file_name = @"C:\Documents and Settings\sandipan\Desktop\WORK\SURVEY.txt";
            //dpl_col = new DrawPolyLineCollection(file_name, GetCurrentIndexes);

            ColumnIndexes colIndex = new ColumnIndexes();
            colIndex.Point_Index = 0;
            colIndex.X_Index = 1;
            colIndex.Y_Index = 2;
            colIndex.Z_Index = 3;
            colIndex.Layer_Index = 4;
            dpl_col = new DrawPolyLineCollection(file_name, colIndex);

            string last_str = "";

            CLabtype lab;
            CModType mod;
            CStgType stg;
            CPTStype pts;
            CTXTtype txt;
            ClSTtype lst;

            List<CLabtype> listlabs = new List<CLabtype>();
            List<ClSTtype> listLst = new List<ClSTtype>();

            string model_name = "GROUND";
            try
            {

                for (int i = 0; i < dpl_col.Count; i++)
                {
                    dpl_col[i].LayerName = dpl_col[i].Label;



                    if ((dpl_col[i].PointNo == 0) ||
                        ((dpl_col[i].X <= -999 || dpl_col[i].X == 0.0) &&
                        (dpl_col[i].Y <= -999 || dpl_col[i].Y == 0.0)))
                    {
                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.EndCode;
                        listlabs.Add(lab);
                    }
                    else
                    {
                        if (dpl_col[i].LayerName != last_str )
                        {
                            last_str = dpl_col[i].LayerName;

                            mod = new CModType();
                            mod.Name = model_name;

                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.Model;
                            lab.Tag = mod;
                            listlabs.Add(lab);

                            stg = new CStgType();
                            stg.label = last_str;

                            lab = new CLabtype();
                            lab.attr = CLabtype.Type.String;
                            lab.Tag = stg;
                            listlabs.Add(lab);


                            lst = new ClSTtype();
                            lst.strMod1 = model_name;
                            lst.strStg = last_str;
                            listLst.Add(lst);
                        }

                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.Point;

                        pts = new CPTStype();

                        pts.mx = dpl_col[i].X;
                        pts.my = dpl_col[i].Y;
                        pts.mz = dpl_col[i].Z;
                        pts.mc = 123.00;
                        lab.Tag = pts;
                        listlabs.Add(lab);
                    }
                }
                lab = new CLabtype();
                lab.attr = CLabtype.Type.EndCode;
                listlabs.Add(lab);

                string pth = Path.Combine(WorkingFolder, "MODEL.LST");
                WriteModelLstFile(pth, listLst);
                pth = Path.Combine(WorkingFolder, "MODEL.FIL");
                WriteModelFilFile(pth, listlabs);
            }
            catch (Exception ex) { }
            finally
            {

            }
        }

        void WriteModelLstFile(string strFilePath, List<ClSTtype> listData)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strFilePath, FileMode.Append), Encoding.Default);
            foreach (ClSTtype lstdata in listData)
            {
                lstdata.ToStream(bw);
            }
            bw.Close();
        }
      
        void WriteModelFilFile(string strPath, List<CLabtype> listfil)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Append), Encoding.Default);
            for (int i = 0; i < listfil.Count; i++)
            {
                CLabtype lab = listfil[i];
                lab.ToStream(bw);
            }
            bw.Close();
        }

        #endregion

        #region Reverse Polyline
        public static void ReversePolyline(vdDocument vdoc)
        {
            vdFigure vFig;
            gPoint gp = new gPoint();

            vdoc.Prompt("Select a Polyline:");

            vdoc.ActionUtility.getUserEntity(out vFig, out gp);

            IvdPolyline pLine = vFig as vdPolyline;

            if (pLine is vdPolyline)
            {
                pLine.VertexList.Reverse();
                vdoc.Redraw(true);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
                MessageBox.Show("Reverse Process completed.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Data To Polyline
        void SetIndex(string select_text, int col_indx)
        {
            //0     Point Nos.
            //1     Chainage
            //2     X
            //3     Y
            //4     Z
            //5     Lable
            //6     Segment Lengths
            //7     Layer
            //8     Height
            //9     None (Default)

            switch (select_text)
            {
                case "Point Nos.":
                    indx.Point_Index = col_indx;
                    break;
                case "Chainage":
                    indx.Chainage_Index = col_indx;
                    break;
                case "X":
                    indx.X_Index = col_indx;
                    break;
                case "Y":
                    indx.Y_Index = col_indx;
                    break;
                case "Z":
                    indx.Z_Index = col_indx;
                    break;
                case "Label":
                    indx.Label_Index = col_indx;
                    break;
                case "Segment Lengths":
                    indx.Segment_Index = col_indx;
                    break;
                case "Layer":
                    indx.Layer_Index = col_indx;
                    break;
                case "Height":
                    indx.Height_Index = col_indx;
                    break;
            }
        }
        void SetIndex(int select_index, int col_indx)
        {
            //0     Point Nos.
            //1     Chainage
            //2     X
            //3     Y
            //4     Z
            //5     Lable
            //6     Segment Lengths
            //7     Layer
            //8     Height
            //9     None (Default)

            switch (select_index)
            {
                case 0:
                    indx.Point_Index = col_indx;
                    break;
                case 1:
                    indx.Chainage_Index = col_indx;
                    break;
                case 2:
                    indx.X_Index = col_indx;
                    break;
                case 3:
                    indx.Y_Index = col_indx;
                    break;
                case 4:
                    indx.Z_Index = col_indx;
                    break;
                case 5:
                    indx.Label_Index = col_indx;
                    break;
                case 6:
                    indx.Segment_Index = col_indx;
                    break;
                case 7:
                    indx.Layer_Index = col_indx;
                    break;
                case 8:
                    indx.Height_Index = col_indx;
                    break;
            }
        }
        void CreateLayer(string lay_name)
        {
            vdLayer ld = vDoc.Layers.FindName(lay_name);
            if (ld == null)
            {
                ld = new vdLayer();
                ld.SetUnRegisterDocument(vDoc);
                ld.setDocumentDefaults();
                ld.Name = lay_name;
                vDoc.Layers.AddItem(ld);
            }
        }
        void DataToPolyLine()
        {
            vdPolyline pLine = new vdPolyline();
            
            pLine.SetUnRegisterDocument(vDoc);
            pLine.setDocumentDefaults();
            for (int i = 0; i < dpl_col.Count; i++)
            {
                //if (dpl_col[i].X == 0.0 && dpl_col[i].Y == 0.0)
                CreateLayer(dpl_col[i].Label);
                if (dpl_col[i].Z == -999.0)
                {
                    dpl_col[i].Z = 0.0;
                }
                if (dpl_col[i].Label.ToUpper().StartsWith("P"))
                {
                    if (dpl_col[i].X != 0.0 && dpl_col[i].Y != 0.0)
                    {
                        vdPoint dp = new vdPoint();
                        dp.SetUnRegisterDocument(vDoc);
                        dp.setDocumentDefaults();
                        dp.InsertionPoint = new gPoint(dpl_col[i].X, dpl_col[i].Y, dpl_col[i].Z);
                        dp.Layer = vDoc.Layers.FindName(dpl_col[i].Label);
                        vDoc.ActiveLayOut.Entities.AddItem(dp);

                        vdText vtxt = new vdText();
                        vtxt.SetUnRegisterDocument(vDoc);
                        vtxt.setDocumentDefaults();
                        vtxt.InsertionPoint = new gPoint(dpl_col[i].X, dpl_col[i].Y, dpl_col[i].Z);
                        vtxt.Layer = vDoc.Layers.FindName(dpl_col[i].Label);
                        vtxt.TextString = dpl_col[i].Z.ToString();
                        vDoc.ActiveLayOut.Entities.AddItem(vtxt);
                    }


                    //vDoc.CommandAction.CmdPoint();
                    //vDoc.CommandAction.CmdText(dpl_col[i].Z.ToString() ,new gPoint(dpl_col[i].X, dpl_col[i].Y, dpl_col[i].Z), 0.0);
                }
                else if (dpl_col[i].X == 0.0 || dpl_col[i].Y == 0.0)
                {
                    pLine.Layer = vDoc.Layers.FindName(dpl_col[i].Label);
                    vDoc.ActiveLayOut.Entities.AddItem(pLine);
                    //vDoc.Redraw(true);
                    //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
                    //break;
                    pLine = new vdPolyline();
                    pLine.SetUnRegisterDocument(vDoc);
                    pLine.setDocumentDefaults();
                }
                else
                    pLine.VertexList.Add(new VectorDraw.Geometry.gPoint(dpl_col[i].X, dpl_col[i].Y, dpl_col[i].Z));

            }
            vDoc.ActiveLayOut.Entities.AddItem(pLine);
            vDoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
        }
        void FillGrid()
        {
            //StreamReader sr = new StreamReader(new FileStream(txt_select_file.Text, FileMode.Open));
            List<string> list_con = new List<string>(File.ReadAllLines(txt_select_file.Text));
            string kStr = "";
            MyStrings mlist = null;
            try
            {
                dataGridView2.Rows.Clear();
                lbl_moredata.Visible = true;
                for (int i = 0; i < 20; i++)
                {
                    kStr = list_con[i];
                   
                    kStr = MyStrings.RemoveAllSpaces(kStr).Replace(",", " ");

                    mlist = new MyStrings(kStr, ' ');

                    dataGridView2.Rows.Add(mlist.StringList.ToArray());
                    if (i == 0)
                    {
                        int indx = mlist.StringList.IndexOf("Serial");
                        try
                        {
                            //dataGridView1[0, indx].Value = "Point Nos.";
                            //indx = mlist.StringList.IndexOf("Easting");
                            //dataGridView1[1, indx].Value = "X";
                            //indx = mlist.StringList.IndexOf("Northing");
                            //dataGridView1[2, indx].Value = "Y";
                            //indx = mlist.StringList.IndexOf("Elevation");
                            //dataGridView1[3, indx].Value = "Z";
                            //indx = mlist.StringList.IndexOf("Feature");
                            //dataGridView1[4, indx].Value = "Label";

                            dataGridView1.Rows.Add("Point Nos.", "X", "Y", "Z", "Label");
                        }
                        catch (Exception ex) { }

                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                lbl_moredata.Visible = (list_con.Count > 20);
                list_con.Clear();
                list_con = null;
            }
        }
        #endregion

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView2.FirstDisplayedScrollingColumnIndex;
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("");
        }
    }
    public class DrawPolyLine
    {
        private int pts_no;
        private double chainage, x, y, z, seg_len, elevation, height;
        string label, layer_name, text_string;
        double text_rotation;
        DrawAs dra_as = DrawAs.Text;
        public DrawPolyLine()
        {
            pts_no = 0;
            chainage = 0;
            x = 0;
            y = 0;
            z = 0;
            seg_len = 0;
            elevation = 0;
            height = -1.0;
            text_rotation = -1.0;


            label = "";
            layer_name = "";
            text_string = "";
        }
        public int PointNo
        {
            get
            {
                return pts_no;
            }
            set
            {
                pts_no = value;
            }
        }
        public double Chainge
        {
            get
            {
                return chainage;
            }
            set
            {
                chainage = value;
            }
        }
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
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }
        public double SegmentLength
        {
            get
            {
                return seg_len;
            }
            set
            {
                seg_len = value;
            }
        }
        public string LayerName
        {
            get
            {
                return layer_name;
            }
            set
            {
                layer_name = value;
                layer_name = layer_name.Replace(' ', '_');
            }
        }
        public double TextHeight
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }
        public string TextString
        {
            get
            {
                return text_string;
            }
            set
            {
                text_string = value;
            }
        }
        public double TextRotation
        {
            get
            {
                return text_rotation;
            }
            set
            {
                text_rotation = value;
            }
        }
        public DrawAs DrawFormat
        {
            get
            {
                return dra_as;
            }
            set
            {
                dra_as = value;
            }
        }

    }
    public class DrawPolyLineCollection : IList<DrawPolyLine>
    {
        List<DrawPolyLine> list = null;
        string file_name = null;
        public ColumnIndexes indx = null;
        List<string> list_labels = null;
        DrawAs dra_as = DrawAs.Polyline;
        public DrawPolyLineCollection()
        {
            list = new List<DrawPolyLine>();
            list_labels = new List<string>();
            //file_name = FileName;
            //indx = index;
            //ReadFromFile();
        }
        public DrawPolyLineCollection(string FileName, ColumnIndexes index)
        {
            list = new List<DrawPolyLine>();
            file_name = FileName;
            indx = index;
            list_labels = new List<string>();
            ReadFromFile();
        }

        void ReadFromFile()
        {
            List<string> file_content = new List<string>(File.ReadAllLines(file_name));
            MyStrings mList = null;
            DrawPolyLine dpl = null;
            string kStr = "";
            for (int i = 0; i < file_content.Count; i++)
            {
                kStr = MyStrings.RemoveAllSpaces(file_content[i]);
                kStr = kStr.Replace(",", " ");
                mList = new MyStrings(kStr, ' ');
                dpl = new DrawPolyLine();
                try
                {
                    if (mList.Count == 8 || mList.Count >= 9)
                    {
                        dpl.PointNo = (i + 1);
                        if (indx.Chainage_Index != -1)
                            dpl.Chainge = mList.GetDouble(indx.Chainage_Index);

                        if (indx.X_Index != -1)
                            dpl.X = mList.GetDouble(indx.X_Index);

                        if (indx.Y_Index != -1)
                            dpl.Y = mList.GetDouble(indx.Y_Index);

                        if (indx.Z_Index != -1)
                            dpl.Z = mList.GetDouble(indx.Z_Index);

                        if (indx.Label_Index != -1)
                            dpl.Label = mList.StringList[indx.Label_Index];

                        if (indx.Layer_Index != -1)
                            dpl.Label = mList.StringList[indx.Layer_Index];

                        try
                        {
                            //if (mList.Count == 8)
                            //{
                            //    //0    1      2         3         4     5       6      7    
                            //    //1 1.85039 7.69685 -999.00000   HOU    1  0.0394532 HOUSE
                            //    dpl.TextHeight = mList.GetDouble(5);
                            //    dpl.TextRotation = mList.GetDouble(6);
                            //    dpl.TextString = mList.StringList[7];
                            //}
                            if (mList.Count >= 8)
                            {
                                //0    1      2         3         4     5    6      7      8
                                //1 1.85039 7.69685 -999.00000   HOU   HOUSE 1 0.0394532 HOUSE
                                dpl.TextHeight = mList.GetDouble(5);
                                dpl.TextRotation = mList.GetDouble(6);
                                dpl.TextString = mList.GetString(7);
                            }
                        }
                        catch (Exception ex) { }

                        Add(dpl);
                    }
                    else
                    {
                        dpl.PointNo = (i + 1);
                        if (indx.Chainage_Index != -1)
                            dpl.Chainge = mList.GetDouble(indx.Chainage_Index);

                        if (indx.X_Index != -1)
                            dpl.X = mList.GetDouble(indx.X_Index);

                        if (indx.Y_Index != -1)
                            dpl.Y = mList.GetDouble(indx.Y_Index);

                        if (indx.Z_Index != -1)
                            dpl.Z = mList.GetDouble(indx.Z_Index);

                        if (indx.Label_Index != -1)
                            dpl.Label = mList.StringList[indx.Label_Index];

                        if (indx.Layer_Index != -1)
                            dpl.Label = mList.StringList[indx.Layer_Index];

                        Add(dpl);
                    }
                }
                catch (Exception ex) { }

            }
        }
        public string FileName { get { return file_name; } }
        #region IList<DrawPolyLine> Members

        public int IndexOf(DrawPolyLine item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, DrawPolyLine item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DrawPolyLine this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<DrawPolyLine> Members

        public void Add(DrawPolyLine item)
        {
            if (!list_labels.Contains(item.Label))
                list_labels.Add(item.Label);
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DrawPolyLine item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(DrawPolyLine[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DrawPolyLine item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<DrawPolyLine> Members

        public IEnumerator<DrawPolyLine> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        public List<string> Labels
        {
            get
            {
                return list_labels;
            }
        }

        void CreateLayer(vdDocument vDoc, string lay_name)
        {
            vdLayer ld = vDoc.Layers.FindName(lay_name);
            if (ld == null)
            {
                ld = new vdLayer();
                ld.SetUnRegisterDocument(vDoc);
                ld.setDocumentDefaults();
                ld.Name = lay_name;
                vDoc.Layers.AddItem(ld);
            }
        }

        public void DataToPolyLine_All(vdDocument vDoc)
        {
            foreach(string label in list_labels)
            {
                //DrawFormat = drawAs;
                DataToDrawing(vDoc, label);
            }
        }
        public void DataToPolyLine(vdDocument vDoc)
        {
            vdPolyline pLine = new vdPolyline();

            pLine.SetUnRegisterDocument(vDoc);
            pLine.setDocumentDefaults();
            for (int i = 0; i < list.Count; i++)
            {
                CreateLayer(vDoc, list[i].Label);
                if (list[i].Z == -999.0)
                {
                    list[i].Z = 0.0;
                }
                if (list[i].Label.ToUpper().StartsWith("P"))
                {
                    if (list[i].X != 0.0 && list[i].Y != 0.0)
                    {
                        vdPoint dp = new vdPoint();
                        dp.SetUnRegisterDocument(vDoc);
                        dp.setDocumentDefaults();
                        dp.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                        dp.Layer = vDoc.Layers.FindName(list[i].Label);
                        vDoc.ActiveLayOut.Entities.AddItem(dp);

                        vdText vtxt = new vdText();
                        vtxt.SetUnRegisterDocument(vDoc);
                        vtxt.setDocumentDefaults();
                        vtxt.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                        vtxt.Layer = vDoc.Layers.FindName(list[i].Label);
                        vtxt.TextString = list[i].Z.ToString();
                        vDoc.ActiveLayOut.Entities.AddItem(vtxt);
                    }
                }
                else if (list[i].X == 0.0 || list[i].Y == 0.0)
                {
                    pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                    vDoc.ActiveLayOut.Entities.AddItem(pLine);
                    pLine = new vdPolyline();
                    pLine.SetUnRegisterDocument(vDoc);
                    pLine.setDocumentDefaults();
                }
                else
                    pLine.VertexList.Add(new VectorDraw.Geometry.gPoint(list[i].X, list[i].Y, list[i].Z));

            }
            vDoc.ActiveLayOut.Entities.AddItem(pLine);
            vDoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
        }
        public void DataToDrawing(vdDocument vDoc, string label)
        {
            switch (DrawFormat)
            {
                case DrawAs.Polyline:
                    {
                        vdPolyline pLine = new vdPolyline();

                        pLine.SetUnRegisterDocument(vDoc);
                        pLine.setDocumentDefaults();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Label != label) continue;
                            CreateLayer(vDoc, list[i].Label);
                            pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                            if (list[i].Z == -999.0)
                            {
                                list[i].Z = 0.0;
                            }
                            if (list[i].X == 0.0 || list[i].Y == 0.0)
                            {
                                pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                                //if (pLine.Layer == null)
                                //    pLine.Layer = vDoc.Layers.FindName("0");

                                vDoc.ActiveLayOut.Entities.AddItem(pLine);
                                pLine = new vdPolyline();
                                pLine.SetUnRegisterDocument(vDoc);
                                pLine.setDocumentDefaults();
                            }
                            else
                                pLine.VertexList.Add(new VectorDraw.Geometry.gPoint(list[i].X, list[i].Y, list[i].Z));

                        }

                        vDoc.ActiveLayOut.Entities.AddItem(pLine);
                        vDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
                    }
                    break;

                case DrawAs.Point:
                    {
                        vdPoint dp = new vdPoint();
                        vdText vtxt = new vdText();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Label != label) continue;
                            CreateLayer(vDoc, list[i].Label);
                            if (list[i].Z == -999.0)
                            {
                                list[i].Z = 0.0;
                            }
                            if (list[i].X != 0.0 && list[i].Y != 0.0)
                            {
                                dp = new vdPoint();
                                dp.SetUnRegisterDocument(vDoc);
                                dp.setDocumentDefaults();
                                dp.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                                dp.Layer = vDoc.Layers.FindName(list[i].Label);
                                //if (dp.Layer == null)
                                //    dp.Layer = vDoc.Layers.FindName("0");
                                vDoc.ActiveLayOut.Entities.AddItem(dp);

                                //vtxt = new vdText();
                                //vtxt.SetUnRegisterDocument(vDoc);
                                //vtxt.setDocumentDefaults();
                                //vtxt.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                                ////vtxt.Layer = vDoc.Layers.FindName(list[i].Label);
                                //vtxt.TextString = list[i].Z.ToString();
                                //vDoc.ActiveLayOut.Entities.AddItem(vtxt);
                            }
                        }
                    }
                    break;
                case DrawAs.Text:
                    {
                        //vdPoint dp = new vdPoint();
                        vdText vtxt = new vdText();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Label != label) continue;
                            CreateLayer(vDoc, list[i].Label);
                            if (list[i].Z == -999.0)
                            {
                                list[i].Z = 0.0;
                            }
                            if (list[i].X != 0.0 && list[i].Y != 0.0)
                            {
                                //dp = new vdPoint();
                                //dp.SetUnRegisterDocument(vDoc);
                                //dp.setDocumentDefaults();
                                //dp.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                                //dp.Layer = vDoc.Layers.FindName(list[i].Label);
                                //vDoc.ActiveLayOut.Entities.AddItem(dp);
                                vtxt = new vdText();
                                vtxt.SetUnRegisterDocument(vDoc);
                                vtxt.setDocumentDefaults();
                                vtxt.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                                vtxt.Layer = vDoc.Layers.FindName(list[i].Label);
                                //if (vtxt.Layer == null)
                                //    vtxt.Layer = vDoc.Layers.FindName("0");
                                //vtxt.TextString = list[i].Label.ToString();
                                if (list[i].TextHeight != -1.0 && list[i].TextRotation != -1.0)
                                {
                                    vtxt.TextString = list[i].TextString;
                                    vtxt.Height = list[i].TextHeight;
                                    vtxt.Rotation = list[i].TextRotation;
                                }
                                vDoc.ActiveLayOut.Entities.AddItem(vtxt);
                            }
                        }
                    }
                    break;
            }
           
        }

        public void CatchmentDataToDrawing(vdDocument vDoc, string label, Color cl)
        {
            double red, grn, bl;
            red = grn = bl = 0.0;


            switch (DrawFormat)
            {
                case DrawAs.Polyline:
                    {
                        vdPolyline pLine = new vdPolyline();

                        pLine.SetUnRegisterDocument(vDoc);
                        pLine.setDocumentDefaults();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Label != label) continue;
                            CreateLayer(vDoc, list[i].Label);
                            pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                            if (list[i].Z == -999.0)
                            {
                                list[i].Z = 0.0;
                            }
                            if (list[i].X == 0.0 || list[i].Y == 0.0)
                            {
                                pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                                //if (pLine.Layer == null)
                                //    pLine.Layer = vDoc.Layers.FindName("0");
                                ////pLine.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                                ////pLine.HatchProperties.FillColor = new vdColor(cl);
                                //pLine.PenColor = new vdColor(cl);
                                vDoc.ActiveLayOut.Entities.AddItem(pLine);
                                pLine = new vdPolyline();
                                pLine.SetUnRegisterDocument(vDoc);
                                pLine.setDocumentDefaults();
                            }
                            else
                                pLine.VertexList.Add(new VectorDraw.Geometry.gPoint(list[i].X, list[i].Y, list[i].Z));

                        }
                        //pLine.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        //pLine.HatchProperties.FillColor = new vdColor(cl);
                        //pLine.PenColor = new vdColor(cl);
                        vDoc.ActiveLayOut.Entities.AddItem(pLine);
                        vDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
                    }
                    break;
            }

        }
        public void CatchmentDataToDrawing1(vdDocument vDoc, string label, Color cl)
        {
            double red, grn, bl;
            red = grn = bl = 0.0;
       

            switch (DrawFormat)
            {
                case DrawAs.Polyline:
                    {
                        vdPolyline pLine = new vdPolyline();

                        pLine.SetUnRegisterDocument(vDoc);
                        pLine.setDocumentDefaults();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Label != label) continue;
                            CreateLayer(vDoc, list[i].Label);
                            pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                            if (list[i].Z == -999.0)
                            {
                                list[i].Z = 0.0;
                            }
                            if (list[i].X == 0.0 || list[i].Y == 0.0)
                            {
                                pLine.Layer = vDoc.Layers.FindName(list[i].Label);
                                //if (pLine.Layer == null)
                                //    pLine.Layer = vDoc.Layers.FindName("0");
                                pLine.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                                pLine.HatchProperties.FillColor = new vdColor(cl);
                                vDoc.ActiveLayOut.Entities.AddItem(pLine);
                                pLine = new vdPolyline();
                                pLine.SetUnRegisterDocument(vDoc);
                                pLine.setDocumentDefaults();
                            }
                            else
                                pLine.VertexList.Add(new VectorDraw.Geometry.gPoint(list[i].X, list[i].Y, list[i].Z));

                        }
                        pLine.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                        pLine.HatchProperties.FillColor = new vdColor(cl);
                        vDoc.ActiveLayOut.Entities.AddItem(pLine);
                        vDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vDoc);
                    }
                    break;
            }

        }



        public void SelectFromFile(vdDocument vdoc, string file_name, string label)
        {
            vdoc.Blocks.AddFromFile(file_name, false);
            string layer_name = Path.GetFileNameWithoutExtension(file_name);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Label != label) continue;
                CreateLayer(vdoc, layer_name);
                if (list[i].Z == -999.0)
                {
                    list[i].Z = 0.0;
                }
                if (list[i].X != 0.0 && list[i].Y != 0.0)
                {
                    vdInsert vdIns = new vdInsert();
                    vdIns.SetUnRegisterDocument(vdoc);
                    vdIns.setDocumentDefaults();
                    vdIns.Block = vdoc.Blocks[vdoc.Blocks.Count - 1];
                    vdIns.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                    //Chiranjit 2011/02/11, Feb, 11
                    //vdIns.Layer = vdoc.Layers.FindName(layer_name);
                    if (vdIns.Layer == null)
                        vdIns.Layer = vdoc.Layers.FindName("0");
                    vdoc.ActiveLayOut.Entities.AddItem(vdIns);
                }
            }

            
        }
        public DrawAs DrawFormat
        {
            get
            {
                return dra_as;
            }
            set
            {
                dra_as = value;
            }
        }
    }
    public class ColumnIndexes
    {
        int x_indx, y_indx, z_indx, chain_indx, po_indx, label_indx, seg_indx, height_indx, layer_indx;
        public ColumnIndexes()
        {
            chain_indx = -1;
            x_indx = -1;
            y_indx = -1;
            z_indx = -1;
            po_indx = -1; 
            label_indx = -1;
            seg_indx = -1;
            height_indx = -1;
            layer_indx = -1;
        }
        public int X_Index
        {
            get
            {
                return x_indx;
            }
            set
            {
                x_indx = value;
            }
        }
        public int Y_Index
        {
            get
            {
                return y_indx;
            }
            set
            {
                y_indx = value;
            }
        }
        public int Z_Index
        {
            get
            {
                return z_indx;
            }
            set
            {
                z_indx = value;
            }
        }
        public int Chainage_Index
        {
            get
            {
                return chain_indx;
            }
            set
            {
                chain_indx = value;
            }
        }
        public int Point_Index
        {
            get
            {
                return po_indx;
            }
            set
            {
                po_indx = value;
            }
        }
        public int Label_Index
        {
            get
            {
                return label_indx;
            }
            set
            {
                label_indx = value;
            }
        }
        public int Segment_Index
        {
            get
            {
                return seg_indx;
            }
            set
            {
                seg_indx = value;
            }
        }
        public int Layer_Index
        {
            get
            {
                return layer_indx;
            }
            set
            {
                layer_indx = value;
            }
        }
        public int Height_Index
        {
            get
            {
                return height_indx;
            }
            set
            {
                height_indx = value;
            }
        }
    }
    public enum DrawAs
    {
        Polyline = 0,
        Point = 1,
        Text = 2,
        FromFile = 3,
        Line = 4,
    }

    
}
