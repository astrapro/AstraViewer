using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
namespace HEADSNeed.DrawingToData
{
    public interface IPolyApp
    {
        string BlockLibraryFolder { get; set; }
        string SettingFile { get; set; }
        DrawPolyLineCollection PolylineCollection { get; set; }
        vdDocument vDocument { get; set; }
        Settings FileSetting { get; }
        void InsertBlock(string file_name, string label);
        void ReversePolyline();
        void PolylineToText();
        void TextDataToPolyline();
    }

    public class PolyApp : IPolyApp
    {
        string block_lib;
        string set_file;
        DrawPolyLineCollection dplc;
        vdDocument vdoc;
        Settings file_sett = null;
        public PolyApp(vdDocument vdoc)
        {
            this.vdoc = vdoc;
            dplc = new DrawPolyLineCollection();
            set_file = "";
            block_lib = "";
            file_sett = new Settings();
        }
        #region IPolyApp Members

        public string BlockLibraryFolder
        {
            get
            {
                return block_lib;
            }
            set
            {
                if (Directory.Exists(value))
                    block_lib = value;
                else
                    throw new DirectoryNotFoundException("Directory not found.");
            }
        }

        public string SettingFile
        {
            get
            {
                return set_file;
            }
            set
            {
                if (File.Exists(value))
                    set_file = value;
                else
                    throw new FileNotFoundException("File not found.");
            }
        }

        public DrawPolyLineCollection PolylineCollection
        {
            get 
            {
                return dplc;
            }
            set
            {
                dplc = value;
            }
        }

        public vdDocument vDocument
        {
            get
            {
                return vdoc;
            }
            set
            {
                vdoc = value;
            }
        }
        public Settings FileSetting
        {
            get
            {
                return file_sett;
            }
        }

        public void InsertBlock(string file_name, string label)
        {
            throw new NotImplementedException();
        }

        public void ReversePolyline()
        {
            throw new NotImplementedException();
        }

        public void PolylineToText()
        {
            throw new NotImplementedException();
        }

        public void TextDataToPolyline()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [Serializable]
    public class Settings : IList<Setting>
    {
        List<Setting> list = null;
        public Settings()
        {
            list = new List<Setting>();
        }

        #region IList<Setting> Members

        public int IndexOf(Setting item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Label_Name == item.Label_Name)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, Setting item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Setting this[int index]
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

        #region ICollection<Setting> Members

        public void Add(Setting item)
        {
            //int indx = IndexOf(item);
            //if (indx != -1)
            //    list.RemoveAt(indx);
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Setting item)
        {
            return false;
        }

        public void CopyTo(Setting[] array, int arrayIndex)
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

        public bool Remove(Setting item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Setting> Members

        public IEnumerator<Setting> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        public static Settings DeSerialise(string file_name)
        {
            BinaryFormatter brn = new BinaryFormatter();
            Stream stream = new FileStream(file_name, FileMode.Open);
            Settings setting = null;
            try
            {
                setting =(Settings) brn.Deserialize(stream);
            }
            catch (Exception ex) { }
            finally
            {
                stream.Close();
            }
            return setting;
        }
        public static void Serialise(string file_name, Settings obj)
        {
            BinaryFormatter brn = new BinaryFormatter();
            Stream stream = new FileStream(file_name, FileMode.Create);
            try
            {
                brn.Serialize(stream, obj);
            }
            catch (Exception ex) { }
            finally
            {
                stream.Flush();
                stream.Close();
            }
        }
    }
    
    [Serializable]
    public class Setting
    {
        string layer_name, draw_element, label_name;
        public Setting()
        {
            layer_name = "";
            draw_element = "";
            label_name = "";
            IsDraw = true;
        }
        public Setting(string layer_name, string draw_element, string label_name)
        {
            this.layer_name = layer_name;
            this.draw_element = draw_element;
            this.label_name = label_name;
        }
        public bool IsDraw { get; set; }
        public string Layer_Name
        {
            get
            {
                return layer_name;
            }
            set
            {
                layer_name = value;
            }
        }
        public string Label_Name
        {
            get
            {
                return label_name;
            }
            set
            {
                label_name = value;
            }
        }
        public string Drawing_Element
        {
            get
            {
                return draw_element;
            }
            set
            {
                draw_element = value;
            }
        }
    }
}
