using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;


using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra.FORMS;



namespace HEADSNeed.ASTRA.CadToAstra
{
    public class DrawingMenu
    {
        string _menu = "";
        vdDocument doc = null;
        string file_name = "";

        Form main_form = null;
        ASTRADoc AstDoc = null;
        public DrawingMenu()
        {
            _menu = "";
            AstDoc = new ASTRADoc();
        }
        public void Menu(string menu,vdDocument vdDoc)
        {
            //
            _menu = menu.Replace("DD_", "").Trim().TrimEnd().TrimStart();
            doc = vdDoc;

            switch (_menu)
            {
                case "JointCoordinates":
                    JointCoordinates(doc);
                    break;
                case "MemberIncidence":
                    MemberIncidence(doc);
                    break;
                case "SectionProperties":
                    SectionProperties(doc);
                    break;
                case "Support":
                    Support(doc);
                    break;
            }
        }
        public vdDocument Doc
        {
            get
            {
                return doc;
            }
        }

        #region Drawing Menu
        public void JointCoordinates(vdDocument doc)
        {
        }
        public void MemberIncidence(vdDocument doc)
        {
        }
        public void SectionProperties(vdDocument doc)
        {
            //frmSectionProperties f_sec_prop = new frmSectionProperties(this);
            //f_sec_prop.Owner = Main_Form;
            //f_sec_prop.Show();
        }
        public void MaterialProperties(vdDocument doc)
        {
            //frmMaterialProperties f_mat_prop = new frmMaterialProperties(doc, AstDoc);
            //f_mat_prop.Owner = Main_Form;
            //f_mat_prop.Show();
        }
        public void Support(vdDocument doc)
        {
            //frmSupport f_support = new frmSupport(doc, AstDoc);
            //f_support.Owner = Main_Form;
            //f_support.Show();
        }
        #endregion

        public string File_Name
        {
            get
            {
                file_name = Path.Combine(Application.StartupPath, "DrawCAD");
                if (!Directory.Exists(file_name))
                {
                    Directory.CreateDirectory(file_name);
                }

                file_name = Path.Combine(file_name, "DrawCAD.txt");
                return file_name;
            }
        }
        public void RemoveAllTexts(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    txt.Deleted = true;
                }
            }
            doc.Redraw(true);
        }
      
        public void DrawJointText(vdDocument doc, ASTRADoc ast_doc)
        {
            RemoveAllTexts(doc);
            File_Save_Open(doc);
            ast_doc.Joints.DrawJointsText(doc, 0.9d);
        }
        public void File_Save_Open(vdDocument doc)
        {
            string f_name = doc.FileName;

            doc.SaveAs(f_name);
            doc.ClearAll();
            doc.ClearEraseItems();
            doc.Open(f_name);
        }
        public Form Main_Form
        {
            get
            {
                return main_form;
            }
            set
            {
                main_form = value;
            }
        }
    }
}
