using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.LSection
{
    # region enums
    internal enum LSTTypeAttributeSize
    {
        ModSize = 30,
        StgSize = 20
    }
    # endregion

    internal class LSTTypeItem
    {
        # region private data
        private string mod = string.Empty;
        private List<string> stg = new List<string>();
        # endregion

        # region properties
        public string Mod
        {
            get { return this.mod; }
            set { this.mod = value; }
        }
        public List<string> Stg
        {
            get { return this.stg; }
            set { this.stg = value; }
        }
        # endregion

        # region construction
        public LSTTypeItem() { }
        public LSTTypeItem(string mod, string stg)
        {
            this.mod = mod;
            this.stg.Add(stg);
        }
        # endregion
    }

    internal class LSTTypeItemCollection : List<LSTTypeItem>
    {
        # region construction
        # endregion

        # region indexers
        public LSTTypeItem this[string mod]
        {
            get
            {
                LSTTypeItem result = null;
                foreach(LSTTypeItem item in this)
                {
                    if (item.Mod == mod)
                    {
                        result = item;
                    }
                }
                return result;
            }
        }
        # endregion

        # region APIs
        public bool LoadFile(string path)
        {
            bool bSuccess = false;
            if (File.Exists(path))
            {
                BinaryReader br = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.Default);
                while(br.BaseStream.Position < br.BaseStream.Length)
                {
                    string mod = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
                    string stg = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

                    if (string.IsNullOrEmpty(mod) || string.IsNullOrEmpty(stg))
                    {
                        continue;
                    }
                    if (this.Contains(mod) == false)
                    {
                        LSTTypeItem item = new LSTTypeItem(mod, stg);
                        this.Add(item);
                    }
                    else
                    {
                        this[mod].Stg.Add(stg);
                    }
                }

                bSuccess = true;
            }
            return bSuccess;
        }
        
        public LSTTypeItem Add(string mod, string stg)
        {
            LSTTypeItem item = new LSTTypeItem(mod, stg);
            this.Add(item);
            return item;
        }
        public bool Contains(string model)
        {
            bool result = false;
            foreach (LSTTypeItem item in this)
            {
                if (item.Mod == model)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        # endregion

        
    }
}
