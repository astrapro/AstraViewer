using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using HeadsFunctions1.Properties;
using System.IO;
using HeadsFunctions1;



namespace HeadsFunctions1.FindOption
{
    public partial class FormFindOption : Form
    {
        private IHeadsApplication m_app;
        int index = -1;
        string find_text = "";
        string last_text = "";
        bool flag_find = false;
        public FormFindOption(IHeadsApplication thisApp)
        {
            InitializeComponent();
            m_app = thisApp;
        }

        private void btn_Find_Click(object sender, EventArgs e)
        {
            //if (index == -1) index = 0;
            //index = m_app.ActiveDocument.Search_Text(txt_search_text.Text, ZoomFactor, index+1);

        }
        //public int ZoomFactor
        //{
        //    get
        //    {
        //        //return ((100 - (cmb_zoom_factor.SelectedIndex + 1)));
        //    }
        //}
        private void FormFindOption_Load(object sender, EventArgs e)
        {
            //for (int i = 1; i <= 100.0; i++) cmb_zoom_factor.Items.Add(i);

            //cmb_zoom_factor.SelectedIndex = 49;

            ReadData();
        }

        private void btn_Find_Next_Click(object sender, EventArgs e)
        {
            find_text = "";
            index = m_app.ActiveDocument.Search_Text(txt_search_text.Text, 0, index + 1, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);

            //index = m_app.ActiveDocument.Search_Text(txt_search_text.Text, ZoomFactor, index + 1, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);

            //object ff = m_app.ActiveDocument.Search_Entity(txt_search_text.Text, ZoomFactor, index + 1, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);



            if (last_text == txt_search_text.Text)
            {
                if (index > 0)
                    flag_find = true;
            }
            else
                flag_find = false;

            last_text = txt_search_text.Text;

            tsslbl_find_text.Text = find_text;

            if (index == -1)
            {
                if (flag_find == false)
                    MessageBox.Show("The following specified text was not found :\n\n " + txt_search_text.Text, "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    MessageBox.Show("Find reached the starting point of the search.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    index = -1;
                }
            }

            string txt = txt_search_text.Text;

           
                txt_search_text.Items.Remove(txt);

            if (txt_search_text.Items.Count == 0)
                txt_search_text.Items.Add(txt);
            else
                txt_search_text.Items.Insert(0, txt);

            txt_search_text.Text = txt;


            //}

        }

        private void btn_Replace_Next_Click(object sender, EventArgs e)
        {
            find_text = "";
            index = m_app.ActiveDocument.Replace_Text(txt_search_text.Text, txt_replace_text.Text, 0, index, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);

            if (last_text == txt_search_text.Text)
            {
                if (index > 0)
                    flag_find = true;
            }
            else
                flag_find = false;

            last_text = txt_search_text.Text;

            tsslbl_find_text.Text = find_text;

            if (index == -1)
            {
                if (flag_find == false)
                    MessageBox.Show("The following specified text was not found :\n\n " + txt_search_text.Text, "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    MessageBox.Show("Find reached the starting point of the search.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    index = -1;
                }
            }


            string txt = txt_search_text.Text;
            //txt_search_text.Items.Remove(txt);


            if (txt_search_text.Items.Contains(txt))
            {
                txt_search_text.Items.Remove(txt);
            }


            if (txt_search_text.Items.Count == 0)
                txt_search_text.Items.Add(txt);
            else
                txt_search_text.Items.Insert(0, txt);

            txt_search_text.Text = txt;

            txt = txt_replace_text.Text;


            if (txt_replace_text.Items.Contains(txt))
            {
                txt_replace_text.Items.Remove(txt);
            }

            if (txt_replace_text.Items.Count == 0)
                txt_replace_text.Items.Add(txt);
            else
                txt_replace_text.Items.Insert(0, txt);
            txt_replace_text.Text = txt;
        }

        private void btn_Replace_All_Click(object sender, EventArgs e)
        {

            find_text = "";
            //index = m_app.ActiveDocument.Replace_All(txt_search_text.Text, txt_replace_text.Text, 0, index + 1, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);


            index = m_app.ActiveDocument.Replace_All(txt_search_text.Text, txt_replace_text.Text, 0, 0, chk_match_case.Checked, chk_match_whole_word.Checked, out find_text);

            if (last_text == txt_search_text.Text)
            {
                if (index > 0)
                    flag_find = true;
            }
            else
                flag_find = false;

            last_text = txt_search_text.Text;

            tsslbl_find_text.Text = find_text;

            if (index == -1)
            {
                if (flag_find == false)
                    MessageBox.Show("The following specified text was not found :\n\n " + txt_search_text.Text, "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    MessageBox.Show("Find reached the starting point of the search.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    index = -1;
                }
            }
            else if (index == 1)
            {
                MessageBox.Show("All Texts are Replaced.", "HEADS Viewer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



            string txt = txt_search_text.Text;
            //txt_search_text.Items.Remove(txt);


            txt_search_text.Items.Remove(txt);


            if (txt_search_text.Items.Count == 0)
                txt_search_text.Items.Add(txt);
            else
                txt_search_text.Items.Insert(0, txt);
            txt_search_text.Text = txt;

            txt = txt_replace_text.Text;


            txt_replace_text.Items.Remove(txt);

            if (txt_replace_text.Items.Count == 0)
                txt_replace_text.Items.Add(txt);
            else
                txt_replace_text.Items.Insert(0, txt);

            txt_replace_text.Text = txt;

        }


        public void SaveData()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < txt_search_text.Items.Count; i++)
            {
                list.Add(txt_search_text.Items[i].ToString());
            }

            string file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "find.rcnt");

            File.WriteAllLines(file_name, list.ToArray());


            list.Clear();
            for (int i = 0; i < txt_replace_text.Items.Count; i++)
            {
                list.Add(txt_replace_text.Items[i].ToString());
            }

            file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "replace.rcnt");

            File.WriteAllLines(file_name, list.ToArray());
        }
        public void ReadData()
        {

            List<string> list = new List<string>();

            string file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "find.rcnt");

            if (File.Exists(file_name))
            {

                list.AddRange(File.ReadAllLines(file_name));
            }

            foreach (var item in list)
            {
                if(item != "")
                txt_search_text.Items.Add(item);
            }

            file_name = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "replace.rcnt");

            list.Clear();
            if (File.Exists(file_name))
            {
                list.AddRange(File.ReadAllLines(file_name));
            }

            foreach (var item in list)
            {
                if (item != "")
                    txt_replace_text.Items.Add(item);
            }

        }
    }
}
