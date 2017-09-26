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

namespace HEADSNeed.DisNet
{
    class DisNetCommands
    {
    }
    public class ActionDisNetCommands : ActionEntityEx
    {
        private vdLine figure;

        private vdSectionClip secClip;

        public ActionDisNetCommands(gPoint startPoint, vdLayout layout, gPoint endPoint)
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
        public static void CmdLoopOn(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "Loops")
                {
                    doc.Layers[i].Frozen = false;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        public static void CmdLoopOff(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "Loops")
                {
                    doc.Layers[i].Frozen = true;
                    doc.Redraw(true);
                    return;
                }
            }
        }
       
        public static void CmdPipeDetailsOn(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "PipeData")
                {
                    doc.Layers[i].Frozen = false;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        public static void CmdPipeDetailsOff(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "PipeData")
                {
                    doc.Layers[i].Frozen = true;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        
        public static void CmdNodalDataOn(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "NodalData")
                {
                    doc.Layers[i].Frozen = false;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        public static void CmdNodalDataOff(vdDocument doc)
        {
            for (int i = 0; i < doc.Layers.Count; i++)
            {

                if (doc.Layers[i].Name == "NodalData")
                {
                    doc.Layers[i].Frozen = true;
                    doc.Redraw(true);
                    return;
                }
            }
        }
        

        
        //Hare Krishna Hare Krishna
        //Krishna Krishna Hare Hare

        //Hare Rama Hare Rama
        //Hare Rama Hare Rama
    }

}
