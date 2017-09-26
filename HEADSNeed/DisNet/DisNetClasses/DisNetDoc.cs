using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HEADSNeed.DisNet.DisNetClasses
{
    public class DisNetDoc
    {
        DisNetLoopCollection loops = null;
        DisNetNodeCollection nodes = null;
        DisNetPipeDetailsCollection pipes = null;
        string sFileName = "";
        public DisNetDoc(string fileName)
        {
            loops = new DisNetLoopCollection();
            nodes = new DisNetNodeCollection();
            pipes = new DisNetPipeDetailsCollection();
            sFileName = fileName;
        }
        public bool ReadFromFile()
        {
            bool success = false;
            if (!File.Exists(sFileName)) return success;

            string kStr = "";
            string option = "";

            StreamReader sr = new StreamReader(new FileStream(sFileName, FileMode.Open, FileAccess.Read));
            try
            {
                while (!sr.EndOfStream)
                {
                    kStr = sr.ReadLine().Trim().TrimEnd().TrimStart();

                    //Node = 1
                    //Elevation = 99.228 m.
                    //Head = 21.000 m.
                    //POINTS = 54005.587,50772.821,100.228
                    if (kStr.StartsWith("Node"))
                    {
                        try
                        {
                            DisNetNode nd = new DisNetNode();
                            nd.SetNodeNo(kStr);
                            nd.SetElevation(sr.ReadLine());
                            nd.SetHead(sr.ReadLine());
                            nd.SetNodePoint(sr.ReadLine());
                            kStr = sr.ReadLine().Trim().TrimEnd().TrimStart();
                            nd.IsPump = kStr.Contains("PUMP");
                            nodes.Add(nd);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    //Pipe = 1
                    //Length = 195.185 m.
                    //Diameter = 0.15 m.
                    //Discharge = 0.0228 m3/s.
                    //POINTS = 54020.713,50675.57,99.228
                    if (kStr.StartsWith("Pipe"))
                    {
                        try
                        {
                            DisNetPipeDetails pipeDtls = new DisNetPipeDetails();
                            pipeDtls.SetPipeNo(kStr);
                            pipeDtls.SetPipeLength(sr.ReadLine());
                            pipeDtls.SetPipeDiameter(sr.ReadLine());
                            pipeDtls.SetPipeDischarge(sr.ReadLine());
                            pipeDtls.SetPipePoint(sr.ReadLine());

                            pipes.Add(pipeDtls);
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    //Loop = 1
                    //POINTS = 54098.236595,50363.406415,54098.236595
                    if (kStr.StartsWith("Loop"))
                    {
                        try
                        {

                            DisNetLoop loop = new DisNetLoop();
                            loop.SetLoopNo(kStr);
                            loop.SetLoopPoint(sr.ReadLine());

                            loops.Add(loop);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                }
                success = true;
            }
            catch (Exception exx)
            {
                success = false;
            }
            finally
            {
                sr.Close();
            }
            return success;

        }

        public DisNetNodeCollection Nodes
        {
            get
            {
                return nodes;
            }
        }
        public DisNetLoopCollection Loops
        {
            get
            {
                return loops;
            }
        }
        public DisNetPipeDetailsCollection Pipes
        {
            get
            {
                return pipes;
            }
        }
    }
}
