using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using VectorDraw.Serialize;
using VectorDraw.Professional;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Constants;
using VectorDraw.Geometry;
using VectorDraw.Render;
using VectorDraw.Professional.Actions;
using VectorDraw.Actions;


using HEADSNeed.ASTRA.ASTRAClasses;

 //Chiranjit 24/08/09
namespace HEADSNeed.ASTRA
{
    class ASTRACommands
    {
    }
    public class ActionASTRACommands : ActionEntityEx
    {
        private vdLine figure;

        private vdSectionClip secClip;

        public ActionASTRACommands(gPoint startPoint, vdLayout layout, gPoint endPoint)
            : base(startPoint, layout)
        {
            ValueTypeProp |= valueType.DISTANCE;
            figure = new vdLine();
            figure.SetUnRegisterDocument(layout.Document);
            figure.setDocumentDefaults();
            figure.StartPoint = startPoint;
            figure.EndPoint = startPoint;
            figure.ExtrusionVector = Vector.CreateExtrusion(startPoint, endPoint);
            figure.Thickness = startPoint.Distance3D(endPoint);
        }
        public static void CmdJointsOn(vdDocument doc)
        {
            vdLayers layrs = doc.Layers;
            foreach (vdLayer vlay in layrs)
            {
                if (vlay.Name == "Nodes")
                {
                    vlay.Frozen = true;
                }
            }
       }
        public static void CmdJointsOff(vdDocument doc)
        {
            vdLayers layrs = doc.Layers;
            for(int i = 0;i < doc.Layers.Count;i++)
            {

                if (doc.Layers[i].Name == "Nodes")
                {
                    doc.Layers[i].Frozen = false;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        public static void CmdDrawMember(vdDocument doc)
        {

            //doc.Palette.Background = System.Drawing.Color.White;
            //doc.Palette.Forground = System.Drawing.Color.Black;
            //doc.Redraw(true);

            //JointCoordinateCollection jntCol;
            //MemberIncidenceCollection memCol;

            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Title = "SELECT ASTRA INPUT Text File";
            //    ofd.Filter = "Text Files|*.txt";
            //    if (ofd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        doc.New();
            //        ASTRADoc astraDoc = new ASTRADoc(ofd.FileName);
            //        //if (MessageBox.Show("Draw Node and Member No?", "HEADS Viewer", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        //{
            //        //    astraDoc.Members.DrawNodeMember = true;
            //        //}
            //        //else
            //        //{
            //        //    astraDoc.Members.DrawNodeMember = false;
            //        //}
            //        //astraDoc.Members.DrawMember(doc);
            //        //astraDoc.Supports.DrawSupport(doc);
            //        //doc.Redraw(true);
            //    }
            //}
        }
      
        public static void CmdMemberLoadOff(vdDocument doc)
        {
            try
            {
                doc.Layers.FindName("MemberLoad").Frozen = true;
                doc.Redraw(true);
            }
            catch (Exception exx) { }

        }
        public static void CmdRunAnalysis(vdDocument doc)
        {
            //doc.SaveAs("C:\\HareKrishna.vdml");
            //doc.SaveAs("C:\\HareKrishna.vdcl");
            //doc.SaveAs("C:\\HareKrishna.dxf");
            //doc.SaveAs("C:\\HareKrishna.dwg");
            //GetExePath();//
        }
        public static void GetExePath()
        {
            
            string defaultLoc = @"C:\Program Files\AsrtA Pro\Release  2009.1.0\AST001.exe";
            string ast001 = System.IO.Path.Combine(Application.StartupPath, "AST001.EXE");

            try
            {
            //    MessageBox.Show(Application.ExecutablePath);
                System.Diagnostics.Process.Start(ast001);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ast001  + " not found in default Location.");
            }
        }
        public static void CmdMemberLoadOn(vdDocument doc)
        {
            try
            {
                doc.Layers.FindName("MemberLoad").Frozen = false;
                doc.Redraw(true);
            }
            catch (Exception exx) { }

        }
        public static void CmdElementOn(vdDocument doc)
        {
            try
            {
                doc.Layers.FindName("Elements").Frozen = false;
                doc.Redraw(true);
            }
            catch (Exception exx) { }

        }
        public static void CmdElementOff(vdDocument doc)
        {
            try
            {
                doc.Layers.FindName("Elements").Frozen = true;
                doc.Redraw(true);
            }
            catch (Exception exx) { }
            
        }


        //Hare Krishna Hare Krishna
        //Krishna Krishna Hare Hare

        //Hare Rama Hare Rama
        //Hare Rama Hare Rama
    }

}
