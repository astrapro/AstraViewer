using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

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

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class Envelope
    {
        List<double> Forces { get; set; }
        public List<int> MembersNos { get; set; }
        public MemberIncidenceCollection Members { get; set; }

        public ASTRADoc AST_Doc { get; set; }
        double factor = 1.0;

        public Envelope(List<double> forces, ASTRADoc astdoc)
        {
            AST_Doc = astdoc;
            Forces = new List<double>(forces.ToArray());
            //Set_Members();



            double max_val = 0.0;


            for (int i = 0; i < forces.Count; i++)
            {
                if(max_val < Math.Abs(forces[i]))
                    max_val = Math.Abs(forces[i]);
            }



            while ((max_val * factor) > 10)
            {
                factor /= 2;
                //max_val = max_val * factor;
            }


            //for (int i = 0; i < forces.Count; i++)
            //{
            //    Forces[i] = forces[i] * factor;
            //}

        }

        public void Set_Members()
        {
            Members = new MemberIncidenceCollection();
          
            foreach (var item in AST_Doc.Members)
            {
                if (MembersNos.Contains(item.MemberNo))
                {
                    Members.Add(item);
                }
            }




        }

        public void DrawEnvelop(vdDocument doc)
        {
            if (Members == null)
                Set_Members();



            vdLayer elementLay = new vdLayer();
            elementLay.Name = "ENVELOPE";
            elementLay.SetUnRegisterDocument(doc);
            elementLay.setDocumentDefaults();
            elementLay.PenColor = new vdColor(Color.DarkGreen);
            doc.Layers.AddItem(elementLay);
            doc.Palette.Background = Color.White;


            VectorDraw.Professional.vdFigures.vdPolyline one3dface = new vdPolyline();
            vdLine ln = null;
            vdMText mtxt = null;


            one3dface.SetUnRegisterDocument(doc);
            one3dface.setDocumentDefaults();

            

            VectorDraw.Geometry.gPoint gp = new VectorDraw.Geometry.gPoint();


            for (var item = 0; item < Members.Count; item++)
            {
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                mtxt = new vdMText();
                mtxt.SetUnRegisterDocument(doc);
                mtxt.setDocumentDefaults();

                mtxt.HorJustify = VdConstHorJust.VdTextHorCenter;

                mtxt.Height = 0.3;
                mtxt.TextString = Forces[item].ToString("f3");
                mtxt.Layer = elementLay;
                   
                gp = new gPoint();
                gp.x = (Members[item].StartNode.Point.x + Members[item].EndNode.Point.x) / 2;
                gp.y = Members[item].StartNode.Point.y;
                gp.z = Members[item].StartNode.Point.z;

                ln.StartPoint = gp;
                
                gp = new gPoint();
                gp.x = (Members[item].StartNode.Point.x + Members[item].EndNode.Point.x) / 2;
                gp.y = Members[item].EndNode.Point.y + Forces[item] * factor;
                gp.z = Members[item].StartNode.Point.z;

                ln.EndPoint = gp;


                mtxt.InsertionPoint = gp;
                doc.ActiveLayOut.Entities.AddItem(mtxt);

                ln.Layer = elementLay;
                doc.ActiveLayOut.Entities.AddItem(ln);

                one3dface.VertexList.Add(gp);

                Members.DrawMember(Members[item], doc);

            }

            one3dface.SPlineFlag = VdConstSplineFlag.SFlagFITTING;

            one3dface.Layer = elementLay;
            doc.ActiveLayOut.Entities.AddItem(one3dface);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);
        }

    }
}
