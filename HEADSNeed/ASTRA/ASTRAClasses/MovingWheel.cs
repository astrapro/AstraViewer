// Hare Krishna
//Chiranjit [2010 01 01]

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
//using System.Drawing;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class MovingWheel
    {
        int type_no;
        string distance, load;
        double width;
        public MovingWheel(int typeNo, string distance_text , string load_text)
        {
            this.type_no = typeNo;
            this.distance = distance_text;
            this.load = load;
            width = 0.0d;
        }
        public MovingWheel()
        {
            this.type_no = 0;
            this.distance = "";
            this.load = "";
            width = 0.0d;
        }
        public int TypeNo
        {
            get
            {
                return type_no;
            }
            set
            {
                type_no = value;
            }
        }

        public string Distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
            }
        }
        public string Load
        {
            get
            {
                return load;
            }
            set
            {
                load = value;
            }
        }
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        public MyStrings List_Distance
        {
            get
            {
                MyStrings mList = new MyStrings(MyStrings.RemoveAllSpaces(distance), ' ');
                return mList;
            }
        }
        public MyStrings List_Load
        {
            get
            {
                MyStrings mList = new MyStrings(MyStrings.RemoveAllSpaces(load), ' ');
                return mList;
            }
        }
        public double Length()
        {
            double len = 0.0;
            try
            {
                for (int i = 0; i < List_Distance.Count; i++)
                {
                    len += List_Distance.GetDouble(i);
                }
            }
            catch (Exception ex)
            {
            }
            return len;
        }

        public double IndexDistance(int index)
        {
            double dist = 0.0d;

            for (int i = 0; i < List_Distance.Count; i++)
            {
                dist += List_Distance.GetDouble(i);
            }
            return dist;
        }
    }
    public class MovingWheelCollection : IList<MovingWheel>
    {
        List<MovingWheel> list = null;

        public MovingWheelCollection()
        {
            list = new List<MovingWheel>();
        }

        #region IList<LoadType> Members

        public int IndexOf(int typeNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TypeNo == typeNo)
                    return i;
            }
            return -1;
        }
        public int IndexOf(MovingWheel item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TypeNo == item.TypeNo)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, MovingWheel item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public MovingWheel this[int index]
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

        #region ICollection<LoadType> Members

        public void Add(MovingWheel item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(MovingWheel item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(MovingWheel[] array, int arrayIndex)
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

        public bool Remove(MovingWheel item)
        {
            int i = IndexOf(item);
            if (i != -1)
            {
                list.RemoveAt(i); 
                return true;
            }
            return false;

        }

        #endregion

        #region IEnumerable<LoadType> Members

        public IEnumerator<MovingWheel> GetEnumerator()
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


        #region Public Method

        public bool ReadTypeDetails(string fileName, int typeNo)
        {
            bool success = false;
            if (!File.Exists(fileName)) return false;

            List<string> lstContent = new List<string>(File.ReadAllLines(fileName));


            string kStr = "";

            MyStrings mList = null;
            for (int i = 0; i < lstContent.Count; i++)
            {
                kStr = MyStrings.RemoveAllSpaces(lstContent[i]).ToUpper();
                if (kStr.StartsWith("TYPE"))
                {
                    kStr = kStr.Replace("TYPE", " ");

                    mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                    if (typeNo == mList.GetInt(0))
                    {
                        MovingWheel mw = new MovingWheel();
                        i++;
                        mw.Load = lstContent[i];
                        i++;
                        mw.Distance = lstContent[i];
                        i++;
                        mw.Width = MyStrings.StringToDouble(lstContent[i], 0.0);
                        Add(mw);
                        success = true;
                        break;
                    }
                }
            }
            return success;
        }



        #endregion
    }
    public class LoadGeneration
    {
        int type_no;
        double x, y, z, x_incr;
        MovingWheelCollection mw = null;
        public LoadGeneration()
        {
            type_no = -1;
            x = y = z = 0.0d;

            mw = new MovingWheelCollection();
        }
        public int TypeNo
        {
            get { return type_no; }
            set { type_no = value; }
        }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        public double XINC
        {
            get { return x_incr; }
            set { x_incr = value; }
        }

        public MovingWheelCollection Moving_Wheel
        {
            get
            {
                return mw;
            }
        }
    }
    public class LoadGenerationCollection : IList<LoadGeneration>
    {
        List<LoadGeneration> list = new List<LoadGeneration>();

        public LoadGenerationCollection()
        {
            list = new List<LoadGeneration>();
        }

        #region IList<LoadGeneration> Members

        public int IndexOf(LoadGeneration item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TypeNo == item.TypeNo)
                    return i;
            }
            return -1;
        }
        public int IndexOf(int type_no)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TypeNo == type_no)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, LoadGeneration item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public LoadGeneration this[int index]
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

        #region ICollection<LoadGeneration> Members

        public void Add(LoadGeneration item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(LoadGeneration item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(LoadGeneration[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count ; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(LoadGeneration item)
        {
            try
            {
                list.RemoveAt(IndexOf(item));
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        #endregion

        #region IEnumerable<LoadGeneration> Members

        public IEnumerator<LoadGeneration> GetEnumerator()
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


        #region Public Method
        public bool Read_Type_From_Text_File(string fileName)
        {
            // Text File
            // Example 7
            //LOAD GENERATION 191
            //TYPE 1 -18.8 0 2.75 XINC 0.2
            //TYPE 1 -18.8 0 6.25 XINC 0.2
            //TYPE 1 -18.8 0 9.75 XINC 0.2 
            
            // AST File
            //LOAD GENERATION 191
            //N012 TYPE 1 -18.8 0 2.75 XINC 0.2
            //N012 TYPE 1 -18.8 0 6.25 XINC 0.2
            //N012 TYPE 1 -18.8 0 9.75 XINC 0.2

            
            if (!File.Exists(fileName)) return false;


            bool IsAst = false;

            if (Path.GetExtension(fileName).ToLower() == ".ast") IsAst = true;

            string ll_txt = Path.GetDirectoryName(fileName);

            ll_txt = Path.Combine(ll_txt, "LL.FIL");
            if (!File.Exists(ll_txt)) ll_txt = Path.Combine(ll_txt, "LL.TXT");


            List<string> lstContent = new List<string>(File.ReadAllLines(fileName));

            bool is_find = false;
            string kStr = "";
            MyStrings mList = null;
            for (int i = 0; i < lstContent.Count; i++)
            {
                kStr = MyStrings.RemoveAllSpaces(lstContent[i]);

                if (kStr.StartsWith("N012"))
                {
                    kStr = MyStrings.RemoveAllSpaces(kStr.Replace("N012", ""));
                } 
                
                if (kStr.Contains("LOAD GENE"))
                {
                    is_find = true; 
                    list.Clear();continue;
                }
                if (kStr.Contains("FINISH"))
                {
                    is_find = false;
                }
                if (is_find)
                {
                    try
                    {
                        mList = new MyStrings(kStr, ' ');
                        LoadGeneration lg = new LoadGeneration();
                        // 0   1    2  3  4     5   6
                        //TYPE 1 -18.8 0 9.75 XINC 0.2
                        lg.TypeNo = mList.GetInt(1);
                        lg.X = mList.GetDouble(2);
                        lg.Y = mList.GetDouble(3);
                        lg.Z = mList.GetDouble(4);
                        lg.XINC = mList.GetDouble(6);
                        lg.Moving_Wheel.ReadTypeDetails(ll_txt, lg.TypeNo);
                        Add(lg);
                    }
                    catch (Exception ex) { is_find = false; }
                }
            }
            return true;
        }


        public void Draw_Moving_Wheel(vdDocument doc, double radius, int load_case, double max_x, double max_y)
        {
            VectorDraw.Professional.Memory.vdMemory.Collect();

            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            hp.FillColor = new vdColor(Color.Black);


            double x = (double)(load_case * 0.2);

            double y = max_y + radius;


            vdCircle cir = new vdCircle();

            #region for loop
            for (int i = 0; i < list[0].Moving_Wheel[0].List_Distance.Count; i++)
            {

                if (x > 0 && x < max_x)
                {
                    for (int z = 0; z < list.Count; z++)
                    {
                        cir = new vdCircle();
                        cir.SetUnRegisterDocument(doc);
                        cir.setDocumentDefaults();
                        cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[z].Z);
                        cir.Radius = radius;
                        cir.HatchProperties = hp;
                        doc.ActiveLayOut.Entities.AddItem(cir);
                    }
                }
                x -= list[0].Moving_Wheel[0].List_Distance.GetDouble(i);
            }
            #endregion

            /*

            if (x > 0 && x < max_x)
            {

                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }

            x -= 3.0;
            if (x > 0 && x < max_x)
            {
                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }

            x -= 3.0;

            if (x > 0 && x < max_x)
            {
                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }

            x -= 4.30;
            if (x > 0 && x < max_x)
            {

                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }

            x -= 1.2;

            if (x > 0 && x < max_x)
            {

                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }
            x -= 3.2;
            if (x > 0 && x < max_x)
            {

                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }
            x -= 1.1;
            if (x > 0 && x < max_x)
            {

                cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                cir.Radius = radius;
                doc.ActiveLayOut.Entities.AddItem(cir);
            }
            //for (int i = 0; i < list[0].Moving_Wheel[0].List_Distance; i++)
            //{
            //    cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new VectorDraw.Geometry.gPoint(((double)(load_case * 0.2)) - 3.0, 3, list[0].Z);
            //    cir.Radius = radius;
            //    doc.ActiveLayOut.Entities.AddItem(cir);
            ////}
              
           /**/
        }
        private void Delete_Rects(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdRect rec = doc.ActiveLayOut.Entities[i] as vdRect;
                if (rec != null)
                {
                    //rec.Deleted = true;
                    //doc.ActiveLayOut.Entities[i].Deleted = true;
                    doc.ActiveLayOut.Entities.RemoveAt(i);
                    i = -1;
                }
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();

        }
        private void Delete_Circles(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdCircle cir = doc.ActiveLayOut.Entities[i] as vdCircle;
                if (cir != null)
                {
                    //rec.Deleted = true;
                    //doc.ActiveLayOut.Entities[i].Deleted = true;
                    doc.ActiveLayOut.Entities.RemoveAt(i);
                    i = -1;
                }
            }
        }
        public void Draw_Plan_Moving_Wheel(vdDocument doc, double height, int load_case, double max_x)
        {
            Delete_Rects(doc);
            double x = (double)(load_case * 0.2);

            double y = 2.0d + height;


            vdRect rect = new vdRect();
            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            hp.FillColor = new vdColor(Color.Black);

            #region for loop
            for (int i = 0; i < list[0].Moving_Wheel[0].List_Distance.Count; i++)
            {

                if (x > 0 && x < max_x)
                {
                    for (int z = 0; z < list.Count; z++)
                    {
                        rect = new vdRect();
                        rect.SetUnRegisterDocument(doc);
                        rect.setDocumentDefaults();
                        rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[z].Z);
                        rect.Height = height;
                        rect.Width = height + 0.2;
                        rect.HatchProperties = hp;

                        rect.ExtrusionVector = new Vector(0, 1, 0);
                        doc.ActiveLayOut.Entities.AddItem(rect);

                        rect = new vdRect();
                        rect.SetUnRegisterDocument(doc);
                        rect.setDocumentDefaults();
                        rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[z].Z + list[z].Moving_Wheel[0].Width);
                        rect.Height = height;
                        rect.Width = height + 0.2;
                        rect.HatchProperties = hp;

                        rect.ExtrusionVector = new Vector(0, 1, 0);
                        doc.ActiveLayOut.Entities.AddItem(rect);

                    }
                }
                x -= list[0].Moving_Wheel[0].List_Distance.GetDouble(i);
            }
            #endregion

            /*
            if (x > 0 && x < max_x)
            {

                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }

            x -= 3.0;
            if (x > 0 && x < max_x)
            {
                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }

            x -= 3.0;

            if (x > 0 && x < max_x)
            {
                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }

            x -= 4.30;
            if (x > 0 && x < max_x)
            {

                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }

            x -= 1.2;

            if (x > 0 && x < max_x)
            {

                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }
            x -= 3.2;
            if (x > 0 && x < max_x)
            {

                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }
            x -= 1.1;
            if (x > 0 && x < max_x)
            {

                rect = new vdRect();
                rect.SetUnRegisterDocument(doc);
                rect.setDocumentDefaults();
                rect.InsertionPoint = new VectorDraw.Geometry.gPoint(x, y, list[0].Z);
                rect.Height = height;
                rect.Width = height;
                rect.ExtrusionVector = new Vector(0, 1, 0);
                doc.ActiveLayOut.Entities.AddItem(rect);
            }
            //for (int i = 0; i < list[0].Moving_Wheel[0].List_Distance; i++)
            //{
            //    cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new VectorDraw.Geometry.gPoint(((double)(load_case * 0.2)) - 3.0, 3, list[0].Z);
            //    cir.Radius = radius;
            //    doc.ActiveLayOut.Entities.AddItem(cir);
            ////}

            /**/
        }
        #endregion
    }
}
