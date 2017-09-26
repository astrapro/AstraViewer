using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using VectorDraw.Geometry;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.DrawingToData
{
    public partial class frmOptions : Form
    {
        ColumnIndexes indx = null;
        vdDocument vdoc = null;
        DrawPolyLineCollection dplc = null;
        //DrawPolyLineCollection text_data = null;
        Settings setting = null;
        string data_file = "";
        bool IsCatchment = false;

        #region form events
        public frmOptions(vdDocument doc, string file_name)
        {
            InitializeComponent();
            vdoc = doc;
            //dplc = dplcol;
            setting = new Settings();
            data_file = file_name;
            IsCatchment = File.Exists(file_name);
        }
        public frmOptions(vdDocument doc)
        {
            InitializeComponent();
            vdoc = doc;
            //dplc = dplcol;
            setting = new Settings();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            cmb_layer.Items.Clear();
            //cmb_label.Items.Add("Select All");
            //cmb_layer.Items.AddRange(dplc.Labels.ToArray());

            //cmb_layer.SelectedIndex = 0;
            //cmb_draw.SelectedIndex = 0;
            if (File.Exists(data_file))
            {
                txt_select_file.Text = data_file;
                FillGrid();
            }
            
        }

        private void btn_layer_color_Click(object sender, EventArgs e)
        {
            //VectorDraw.Professional.Dialogs.LayersDialog.Show(vdoc);
        }

        private void btn_Add_to_list_Click(object sender, EventArgs e)
        {
            string kStr = cmb_layer.Text.ToUpper();
            if (kStr == "SELECT ALL")
            {
                for (int i = 0; i < cmb_layer.Items.Count; i++)
                {
                    kStr = cmb_layer.Items[i].ToString();
                    if (kStr.ToUpper() != "SELECT ALL")
                        dgv_all_data.Rows.Add(true, kStr, cmb_draw.Text, kStr);
                }
            }
            else
                dgv_all_data.Rows.Add(true, cmb_layer.Text, cmb_draw.Text, cmb_layer.Text);

        }

        private void btn_text_style_Click(object sender, EventArgs e)
        {
        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            ReadFromGrid();
            DrawingFromSettingsFile(dplc);
            vdoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
        }
        public void ReadFromGrid()
        {
            setting = new Settings();
            Setting sett = null;
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                sett = new Setting();
                sett.IsDraw = (bool)dgv_all_data[0, i].Value;
                sett.Label_Name = dgv_all_data[1, i].Value.ToString();
                sett.Drawing_Element = dgv_all_data[2, i].Value.ToString();
                sett.Layer_Name = dgv_all_data[3, i].Value.ToString();
                setting.Add(sett);
            }
        }
        
        private void btn_finish_Click(object sender, EventArgs e)
        {
            dplc.Clear();
            dplc = null;
            cmb_draw.Items.Clear();
            cmb_layer.Items.Clear();
            this.Close();
        }

        private void btn_browse_lib_Click(object sender, EventArgs e)
        {
            string block_lib_path = Path.Combine(Application.StartupPath, @"Make Ground Survey Drawing\Block Survey");
            if (!Directory.Exists(block_lib_path))
                block_lib_path = Path.Combine(Application.StartupPath, "Block Survey");
            fbd.SelectedPath = block_lib_path;

            if (fbd.ShowDialog() != DialogResult.Cancel)
            {
                txt_drawing_lib.Text = fbd.SelectedPath;
                LoadDrawingsFromFolder(txt_drawing_lib.Text);
            }
        }

        private void cmb_draw_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grb_from_file.Enabled = (cmb_draw.SelectedIndex == 3);
        }

        private void btn_save_settings_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = "UserSettings1.hus";
                sfd.Filter = "HEADS User Settings Files|*.hus";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    //setting.SaveSettings(sfd.FileName);
                    Settings.Serialise(sfd.FileName, setting);
                }
            }
        }

        private void btn_Sel_setting_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox_read_from_file_CheckedChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region working methods

        void InsertFromFile()
        {
            string file_name = @"C:\TEST\[20101030] Make Ground Survey Drawing\Block Survey\[BH] Bore_Hole.dwg";

            //file_name = Path.Combine(txt_drawing_lib.Text, cmb_draw.Text);
            ////setting.Add(cmb_label.Text, "SELECT_FILE=" + file_name);
            //dplc.SelectFromFile(vdoc, file_name, cmb_label.Text);
        }
        void DrawingFromSettingsFile(DrawPolyLineCollection draw_dplc)
        {
            RefreshItems();
            for (int i = 0; i < setting.Count; i++)
            {
                if (!setting[i].IsDraw)
                {
                    
                        dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    this.Refresh();
                    continue;
                }

                dgv_all_data.FirstDisplayedScrollingRowIndex = i;
                dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;


                this.Refresh();

                switch (setting[i].Drawing_Element.ToUpper())
                {
                    case "POLYLINE":
                        draw_dplc.DrawFormat = DrawAs.Polyline;
                        draw_dplc.DataToDrawing(vdoc, setting[i].Label_Name);
                        break;
                    case "POINT":
                        draw_dplc.DrawFormat = DrawAs.Point;
                        draw_dplc.DataToDrawing(vdoc, setting[i].Label_Name);
                        break;
                    case "TEXT":
                        draw_dplc.DrawFormat = DrawAs.Text;
                        draw_dplc.DataToDrawing(vdoc, setting[i].Label_Name);
                        break;
                    default:
                        draw_dplc.DrawFormat = DrawAs.FromFile;
                        if (File.Exists(Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element)))
                        {
                            draw_dplc.SelectFromFile(vdoc, Path.Combine(txt_drawing_lib.Text, setting[i].Drawing_Element), setting[i].Label_Name);
                        }
                        else
                        {
                            MessageBox.Show("Block Library path is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                        }
                        break;
                }
            }
        }
        void LoadDrawingsFromFolder(string folder)
        {
            cmb_draw.Items.Clear();
            cmb_draw.Items.Add("POLYLINE");
            cmb_draw.Items.Add("POINT");
            cmb_draw.Items.Add("TEXT");
            string ext = "";
            foreach (string nextFile in Directory.GetFiles(folder))
            {
                ext = Path.GetExtension(nextFile).ToLower();
                if (ext == ".vdml" ||
                    ext == ".vdcl" ||
                    ext == ".dxf" ||
                    ext == ".dwg" ||
                    ext == ".jpg" ||
                    ext == ".bmp")
                    cmb_draw.Items.Add(Path.GetFileName(nextFile));
            }
            cmb_draw.SelectedIndex = cmb_draw.Items.Count > 0 ? 0 : -1;
        }

        #endregion

        private void rbtn_select_drawing_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_browse_settings_Click(object sender, EventArgs e)
        {
            ofd.Filter = "HEADS User Settings Files|*.hus";
            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    setting = Settings.DeSerialise(ofd.FileName);
                    txt_select_settings.Text = ofd.FileName;
                    dgv_all_data.Rows.Clear();
                    foreach(Setting sett in setting)
                    {
                        dgv_all_data.Rows.Add(sett.IsDraw , sett.Label_Name, sett.Drawing_Element, sett.Layer_Name);
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("File format is not correct.", "HEADS Viewer", MessageBoxButtons.OK);
                }
            }
        }

        private void btn_delete_rows_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_all_data.Rows.RemoveAt(dgv_all_data.CurrentRow.Index);

            }
            catch (Exception ex) { }
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            //    //dataGridView1.Rows.Re
            //}
        }

        private void chk_Draw_All_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                dgv_all_data[0, i].Value = chk_Draw_All.Checked;
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            RefreshItems();
        }
        void RefreshItems()
        {
            for (int i = 0; i < dgv_all_data.Rows.Count; i++)
            {
                dgv_all_data.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }
            this.Refresh();
        }
        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView1.FirstDisplayedScrollingColumnIndex = dataGridView2.FirstDisplayedScrollingColumnIndex;
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

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            try
            {
                dplc = new DrawPolyLineCollection(txt_select_file.Text, GetCurrentIndexes);
                //cmb_label.Items.Add("Select All");
                cmb_layer.Items.Clear();
                //cmb_layer.Items.Add("Select All");
                List<string> lst = new List<string>(dplc.Labels.ToArray());
                lst.Sort();
                if (lst.Count > 0)
                    lst.Insert(0, "Select All");
                //lst.Insert(0, "Select All");
                cmb_layer.Items.AddRange(lst.ToArray());
               

                cmb_layer.SelectedIndex = 0;
                cmb_draw.SelectedIndex = 0;
            }
            catch (Exception ex) { }
        }
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

    }
}
