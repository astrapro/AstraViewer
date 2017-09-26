using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HeadsUtils
{
    public class Linetype 
    {
        public Linetype()
        {

        }
        public short		elatt;
        public short		scatt;
        public string       layer;//20 char
        public short		laatt;
        public double		x1;
        public double		y1;
        public double		z1;
        public double		x2;
        public double		y2;
        public double		z2;
        public short		color;
        public short		style;
        public short		width;
        public string       label;//20 char

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(elatt);
            bw.Write(scatt);
            bw.Write(ViewerUtils.ConvertStringToByteArray(layer, 20));
            bw.Write(laatt);
            bw.Write(x1);
            bw.Write(y1);
            bw.Write(z1);
            bw.Write(x2);
            bw.Write(y2);
            bw.Write(z2);
            bw.Write(color);
            bw.Write(style);
            bw.Write(width);
            bw.Write(ViewerUtils.ConvertStringToByteArray(label, 20)); 
        }

        public static Linetype FromStream(System.IO.BinaryReader br)
        {
            Linetype line = new Linetype();

            line.elatt = br.ReadInt16();
            line.scatt = br.ReadInt16();
            line.layer = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            line.laatt = br.ReadInt16();
            line.x1 = br.ReadDouble();
            line.y1 = br.ReadDouble();
            line.z1 = br.ReadDouble();
            line.x2 = br.ReadDouble();
            line.y2 = br.ReadDouble();
            line.z2 = br.ReadDouble();
            line.color = br.ReadInt16();
            line.style = br.ReadInt16();
            line.width = br.ReadInt16();
            line.label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return line;
        }

        public CPoint3D StartPoint
        {
            set
            {
                x1 = value.X;
                y1 = value.Y;
                z1 = value.Z;
            }
            get
            {
                return new CPoint3D(x1, y1, z1);
            }
        }

        public CPoint3D EndPoint
        {
            set
            {
                x2 = value.X;
                y2 = value.Y;
                z2 = value.Z;
            }
            get
            {
                return new CPoint3D(x2, y2, z2);
            }
        }
    }

    public class Boxtype 
    {
        public Boxtype()
        {

        }
        public short		elatt;
        public short		scatt;
        public string		Layer;//20 char
        public short		laatt;
        public double		x1;
        public double		y1;
        public double		x2;
        public double		y2;
        public short		color;
        public short		style;
        public short		Width;
        public string       Label;//20 char
        
        public static Boxtype FromStream(System.IO.BinaryReader br)
        {
            Boxtype box = new Boxtype();
            box.elatt = br.ReadInt16();
            box.scatt = br.ReadInt16();
            box.Layer = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            box.laatt = br.ReadInt16();
            box.x1 = br.ReadDouble();
            box.y1 = br.ReadDouble();
            box.x2 = br.ReadDouble();
            box.y2 = br.ReadDouble();
            box.color = br.ReadInt16();
            box.style = br.ReadInt16();
            box.Width = br.ReadInt16();
            box.Label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return box;
        }
    }

    public class Circletype 
    {
        public Circletype()
        {

        }
        public short		elatt;
        public short		scatt;
        public string		Layer;
        public short		laatt;
        public double		xc;
        public double		yc;
        public double		Radius;
        public short		color;
        public short		style;
        public short		Width;
        public string		Label;
        
        public static Circletype FromStream(System.IO.BinaryReader br)
        {
            Circletype circle = new Circletype();

            circle.elatt = br.ReadInt16();
            circle.scatt = br.ReadInt16();
            circle.Layer = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            circle.laatt = br.ReadInt16();
            circle.xc = br.ReadDouble();
            circle.yc = br.ReadDouble();
            circle.Radius = br.ReadDouble();
            circle.color = br.ReadInt16();
            circle.style = br.ReadInt16();
            circle.Width = br.ReadInt16();
            circle.Label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return circle;
        }


        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.elatt);
            bw.Write(this.scatt);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Layer, 20));
            bw.Write(this.laatt);
            bw.Write(this.xc);
            bw.Write(this.yc);
            bw.Write(this.Radius);
            bw.Write(this.color);
            bw.Write(this.style);
            bw.Write(this.Width);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Label, 20));
        }

    }

    public class Arctype 
    {
        public Arctype()
        {

        }
        public short		elatt;
        public short		scatt;
        public string		Layer;
        public short		laatt;
        public double		xs;
        public double		ys;
        public double		xe;
        public double		ye;
        public double		xc;
        public double		yc;
        public double		Radius;
        public short		Color;
        public short		style;
        public short		Width;
        public string		Label;

        public static Arctype FromStream(System.IO.BinaryReader br)
        {
            Arctype arc = new Arctype();

            arc.elatt = br.ReadInt16();
            arc.scatt = br.ReadInt16();
            arc.Layer = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            arc.laatt = br.ReadInt16();
            arc.xs = br.ReadDouble();
            arc.ys = br.ReadDouble();
            arc.xe = br.ReadDouble();
            arc.ye = br.ReadDouble();
            arc.xc = br.ReadDouble();
            arc.yc = br.ReadDouble();
            arc.Radius = br.ReadDouble();
            arc.Color = br.ReadInt16();
            arc.style = br.ReadInt16();
            arc.Width = br.ReadInt16();
            arc.Label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return arc;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.elatt);
            bw.Write(this.scatt);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Layer, 20));
            bw.Write(this.laatt);
            bw.Write(this.xs);
            bw.Write(this.ys);
            bw.Write(this.xe);
            bw.Write(this.ye);
            bw.Write(this.xc);
            bw.Write(this.yc);
            bw.Write(this.Radius);
            bw.Write(this.Color);
            bw.Write(this.style);
            bw.Write(this.Width);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Label, 20));
        }
    }

    public class TEXTtype 
    {
        public TEXTtype()
        {

        }
        public short		Length;
        public short		scatt;
	    public string		Layer;//20
	    public short		laatt;
        public double		x1;
        public double		y1;
        public double		Size;
        public double		rotn;
        public short		Color;
        public short		style;
        public string		text;//60
        public string		Label;//20

        public static TEXTtype FromStream(System.IO.BinaryReader br)
        {
            TEXTtype text = new TEXTtype();

            text.Length = br.ReadInt16();
            text.scatt = br.ReadInt16();
            text.Layer = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            text.laatt = br.ReadInt16();
            text.x1 = br.ReadDouble();
            text.y1 = br.ReadDouble();
            text.Size = br.ReadDouble();
            text.rotn = br.ReadDouble();
            text.Color = br.ReadInt16();
            text.style = br.ReadInt16();
            text.text = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(60));
            text.Label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return text;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.Length);
            bw.Write(this.scatt);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Layer, 20));
            bw.Write(this.laatt);
            bw.Write(this.x1);
            bw.Write(this.y1);
            bw.Write(this.Size);
            bw.Write(this.rotn);
            bw.Write(this.Color);
            bw.Write(this.style);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.text, 60));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.Label, 20));
        }
    }

    public class CCfgtype
    {
        public CCfgtype()
        {
            this.bm = 10.0;
            this.hs = 1.0;
            this.lm = 10.0;
            this.rm = 10.0;
            this.sl = 297.0;
            this.sw = 210.0;
            this.tm = 10.0;
            this.xfa = 0.0;
            this.XLate_X = 0.0;
            this.XLate_Y = 0.0;
            this.XMetric = 1.0;
            this.YMetric = 1.0;
        }
        public double		XMetric = 0;
        public double		YMetric = 0;
        public double		XLate_X = 0;
        public double		XLate_Y = 0;
        public double		xfa = 0;
        public double		sl = 0;
        public double		sw = 0;
        public double		hs = 0;
        public double		bm = 0;
        public double		rm = 0;
        public double		tm = 0;
        public double		lm = 0;
        
        public void CopyTo(CCfgtype cfg)
        {
            cfg.XMetric = this.XMetric;
            cfg.YMetric = this.YMetric;
            cfg.XLate_X = this.XLate_X;
            cfg.XLate_Y = this.XLate_Y;
            cfg.xfa = this.xfa;
            cfg.sl = this.sl;
            cfg.sw = this.sw;
            cfg.hs = this.hs;
            cfg.bm = this.bm;
            cfg.rm = this.rm;
            cfg.tm = this.tm;
            cfg.lm = this.lm;
        }

        public static CCfgtype FromStream(System.IO.BinaryReader br)
        {
            CCfgtype cfg = new CCfgtype();

            cfg.XMetric = br.ReadDouble();
            cfg.YMetric = br.ReadDouble();
            cfg.XLate_X = br.ReadDouble();
            cfg.XLate_Y = br.ReadDouble();
            cfg.xfa = br.ReadDouble();
            cfg.sl = br.ReadDouble();
            cfg.sw = br.ReadDouble();
            cfg.hs = br.ReadDouble();
            cfg.bm = br.ReadDouble();
            cfg.rm = br.ReadDouble();
            cfg.tm = br.ReadDouble();
            cfg.lm = br.ReadDouble();

            return cfg;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.XMetric);
            bw.Write(this.YMetric);
            bw.Write(this.XLate_X);
            bw.Write(this.XLate_Y);
            bw.Write(this.xfa);
            bw.Write(this.sl);
            bw.Write(this.sw);
            bw.Write(this.hs);
            bw.Write(this.bm);
            bw.Write(this.rm);
            bw.Write(this.tm);
            bw.Write(this.lm);
        }
        
    }

    public class ClSTtype
    {
        public ClSTtype()
        {
        }
        public string strMod1 = "";//30
        public string strStg  = "";//20

        public void ToStream(BinaryWriter bw)
        {
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.strMod1, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.strStg, 20));
        }

        public static ClSTtype FromStream(BinaryReader br)
        {
            ClSTtype lstdata = new ClSTtype();

            lstdata.strMod1 = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
            lstdata.strStg = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return lstdata;
        }
    }	

    public class CLabtype
    {
        public enum Type
        {
            Unknown,
            Model,
            String,
            Point,
            Text,
            EndCode,
        }

        public CLabtype()
        {

        }
        private short attrVal = 0;

        public object Tag = null;

        public static CLabtype FromStream(System.IO.BinaryReader br)
        {
            CLabtype lab = new CLabtype();
            lab.attrVal = br.ReadInt16();

            if (lab.attr == Type.Model)
            {
                CModType mod = new CModType();
                mod.Name = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
                lab.Tag = mod;
            }
            else if (lab.attr == Type.String)
            {
                CStgType stg = new CStgType();
                stg.label = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
                lab.Tag = stg;
            }
            else if (lab.attr == Type.Point)
            {
                CPTStype pts = CPTStype.FromStream(br);
                lab.Tag = pts;
            }
            else if (lab.attr == Type.Text)
            {
                CTXTtype txt = CTXTtype.FromStream(br);
                lab.Tag = txt;
            }

            return lab;
        }

        public void ToStream(BinaryWriter bw)
        {
            bw.Write(this.attrVal);
            if (this.attr == Type.Model)
            {
                CModType mod = (CModType)this.Tag;
                bw.Write(ViewerUtils.ConvertStringToByteArray(mod.Name, 30));
            }
            else if (this.attr == Type.String)
            {
                CStgType stg = (CStgType)this.Tag;
                bw.Write(ViewerUtils.ConvertStringToByteArray(stg.label, 20));
            }
            else if (this.attr == Type.Point)
            {
                CPTStype pts = (CPTStype)this.Tag;
                pts.ToStream(bw);
            }
            else if (this.attr == Type.Text)
            {
                CTXTtype txt = (CTXTtype)this.Tag;
                txt.ToStream(bw);
            }           
        }        

        void SetAttrib(Type type)
        {
            if (type == Type.Model)
            {
                if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL
                    || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA)
                {
                    attrVal = 101;
                }
                else if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                {
                    attrVal = 901;
                }
                else
                {
                    throw new Exception("Invalid release type");
                }
            }
            else if (type == Type.String)
            {
                if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL
                    || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA)
                {
                    attrVal = 102;
                }
                else if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                {
                    attrVal = 902;
                }
                else
                {
                    throw new Exception("Invalid release type");
                }
            }
            else if (type == Type.Point)
            {
                attrVal = 103;
            }
            else if (type == Type.Text)
            {
                attrVal = 104;
            }
            else if (type == Type.EndCode)
            {
                attrVal = 999;
            }
            else
            {
                throw new Exception("Invalid attribute type");
            }
        }

        Type GetAttrib()
        {
            Type type = Type.Unknown;

            switch (this.attrVal)
            {
                case 101:
                {
                    if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL
                        || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA
                        || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                    {
                        type = Type.Model;
                    }
                    break;
                }
                case 901:
                {
                    if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                    {
                        type = Type.Model;
                    }
                    break;
                }
                case 102:
                {
                    if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL
                        || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA
                        || HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                    {
                        type = Type.String;
                    }
                    break;
                }
                case 902:
                {
                    if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                    {
                        type = Type.String;
                    }
                    break;
                }
                case 103:
                {
                    type = Type.Point;
                    break;
                }
                case 104:
                {
                    type = Type.Text;
                    break;
                }
                case 999:
                {
                    type = Type.EndCode;
                    break;
                }
            }
            return type;
        }        

        public Type attr
        {
            get
            {
                return this.GetAttrib();
            }
            set
            {
                this.SetAttrib(value);
            }
        }
    };

    public class CPTStype
    {
        public CPTStype()
        {

        }
        public double		mc = 0;
        public double		mx = 0;
        public double		my = 0;
        public double		mz = 0;

        public void ToStream(BinaryWriter bw)
        {
            bw.Write(this.mc);
            bw.Write(this.mx);
            bw.Write(this.my);
            bw.Write(this.mz);
        }

        public static CPTStype FromStream(BinaryReader br)
        {
            CPTStype pts = new CPTStype();

            pts.mc = br.ReadDouble();
            pts.mx = br.ReadDouble();
            pts.my = br.ReadDouble();
            pts.mz = br.ReadDouble();

            return pts;
        }
    };

    /// <summary>
    /// Layer and label changed to CStrings
    /// class introduced dxdeb 13/11/03
    /// </summary>
    public class CLinetype
    {
        public CLinetype()
        {
            Layer = "$$$$$$$$$$$$$$$$$$$$";
            Label = "XXXXXXXXXXXXXXXXXXXX";
        }

        public short elatt;
        public short		scatt;
	    public string		Layer;
        public short		laatt;
        public double		x1;
        public double		y1;
        public double		z1;
        public double		x2;
        public double		y2;
        public double		z2;
        public short		ColorData;
        public short		style;
        public short		Width;
        public string       Label;


        public CPoint3D StartPoint
        {
            set
            {
                x1 = value.X;
                y1 = value.Y;
                z1 = value.Z;
            }
            get
            {
                return new CPoint3D(x1, y1, z1);
            }
        }
        public CPoint3D EndPoint
        {
            set
            {
                x2 = value.X;
                y2 = value.Y;
                z2 = value.Z;
            }
            get
            {
                return new CPoint3D(x2, y2, z2);
            }
        }
    };

    public class CTXTtype
    {
        public CTXTtype()
        {
        }

	    public double		tx = 0;
	    public double		ty = 0;
        public double       tz = 0;
        public double		ts = 0;        
	    public double		tr = 0;
	    public string		tg = "";//60 char array

        public CPoint3D Point
        {
            set
            {
                tx = value.X;
                ty = value.Y;
                tz = value.Z;
            }
            get
            {
                return new CPoint3D(tx, ty, tz);
            }
        }

        public void ToStream(BinaryWriter bw)
        {
            bw.Write(this.tx);
            bw.Write(this.ty);
            bw.Write(this.tz);
            bw.Write(this.ts);
            bw.Write(this.tr);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.tg, 60));
        }

        public static CTXTtype FromStream(BinaryReader br)
        {
            CTXTtype txt = new CTXTtype();

            txt.tx = br.ReadDouble();
            txt.ty = br.ReadDouble();
            txt.tz = br.ReadDouble();
            txt.ts = br.ReadDouble();
            txt.tr = br.ReadDouble();
            txt.tg = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(60));

            return txt;
        }
    }

    public class CStgType
    {
        public string label = string.Empty; //char [20]
    }

    public class CModType
    {
        public string name = string.Empty; //char[30];
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }

    public class CLayOut
    {
        public CLayOut() { }
        public double startX;
        public double startY;
        public double angle;
        public double sheetLength;
        public double sheetWidth;
        public double layOutScale;
        public double leftM;
        public double rightM;
        public double topM;
        public double bottomM;
                
    }

    public class Grid
    {
        public Grid() { }
        public string ModelName;//[30]
        public string StringName;//[20]
        public double MinX;
        public double MinY;
        public double MaxX;
        public double MaxY;
        public double XInterval;
        public double YInterval;
        public double TextSize;
        public double Rotation;

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.ModelName, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.StringName, 20));
            bw.Write(this.MinX);
            bw.Write(this.MinY);
            bw.Write(this.MaxX);
            bw.Write(this.MaxY);
            bw.Write(this.XInterval);
            bw.Write(this.YInterval);
            bw.Write(this.TextSize);
            bw.Write(this.Rotation);
        }

        public static Grid FromStream(System.IO.BinaryReader br)
        {
            Grid grid = new Grid();
            grid.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
            grid.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            grid.MinX = br.ReadDouble();
            grid.MinY = br.ReadDouble();
            grid.MaxX = br.ReadDouble();
            grid.MaxY = br.ReadDouble();
            grid.XInterval = br.ReadDouble();
            grid.YInterval = br.ReadDouble();
            grid.TextSize = br.ReadDouble();
            grid.Rotation = br.ReadDouble();
            return grid;
        }
    }

    public class Detail
    {
        public Detail()
        {

        }
        
        public string ModelName;//[30];
        public string StringName;//[20];
        public double XMin;
        public double YMin;
        public double TextSize;
        public double Rotation;

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.ModelName, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.StringName, 20));
            bw.Write(this.XMin);
            bw.Write(this.YMin);
            bw.Write(this.TextSize);
            bw.Write(this.Rotation);
        }

        public static Detail FromStream(System.IO.BinaryReader br)
        {
            Detail detail = new Detail();
            detail.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
            detail.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            detail.XMin = br.ReadDouble();
            detail.YMin = br.ReadDouble();
            detail.TextSize = br.ReadDouble();
            detail.Rotation = br.ReadDouble();

            return detail;
        }
    }

    public class Chainage
    {
        public Chainage()
        {

        }
        public string ModelName; // 30
        public string StringName;// 20
        public double XMin;
        public double YMin;
        public double ChainageInterval;
        public double TextSize;
        public double Rotation;
    }

    public class CHHipData
    {
        public CHHipData()
        {

        }

        public CHHipData Copy()
        {
            CHHipData obj = new CHHipData();

            obj.xvalue = this.xvalue;
            obj.yvalue = this.yvalue;
            obj.delta = this.delta;
            obj.radius = this.radius;
            obj.leadtrans = this.leadtrans;
            obj.trailtrans = this.trailtrans;

            return obj;

        }
        public double xvalue = 0;
        public double yvalue = 0;
        public double delta = 0;
        public double radius = 0;
        public double leadtrans = 0;
        public double trailtrans = 0;
    } ;

    public class CHalFile
    {
        public CHalFile()
        {

        }

        public CHalFile Copy()
        {
            CHalFile obj = new CHalFile();
            obj.Code = this.Code;
            obj.AcceptCode = this.AcceptCode;
            obj.ModelName = this.ModelName;
            obj.StringLevel = this.StringLevel;
            obj.StartChainage = this.StartChainage;
            obj.ChainageInterval = this.ChainageInterval;
            foreach(CHHipData data in this.ListHip)
            {
                obj.ListHip.Add(data);
            }
            return obj;
        }

        public short Code = 0;
        public short AcceptCode = 0;
        public string ModelName = "";
        public string StringLevel = "";
        public List<CHHipData> ListHip = new List<CHHipData>();
        public double StartChainage = 0;
        public double ChainageInterval = 0;

        public int TotalHIPs
        {
            get { return ListHip.Count; }
        }
    };

    public class CHalignFilData
    {
        public CHalignFilData()
        {

        }
        public short iTurn = 0;	// left = 1 (deviation -ve, radius -ve etc.), right = 2 +ve dev & rad
        public double dDeviation = 0.0; // dB2 -dB1
        public double dB2 = 0.0;	// end bearing 0<dB2<360
        public double dB1 = 0.0;	// start bearing 0<dB1<360
        public double dEndy = 0.0;
        public double dEndx = 0.0;
        public double dStarty = 0.0;
        public double dStartx = 0.0;
        public double dEllength = 0.0;
        public double dRadius = 0.0;
        public double dT2 = 0.0;	// tangent length 2
        public double dT1 = 0.0;	// tangent length 1
        public double dHipy = 0.0;
        public double dHipx = 0.0;
        public double dEndchn = 0.0;
        public double dStartchn = 0.0;
        public short iEltype = 0;
        public int iSlno = 0;
        public string sString = "";
        public string sMod = "";

        public override string ToString()
        {
            string str = sMod + '\t' + this.sString + '\t' + this.iSlno.ToString()
               + '\t' + this.iEltype.ToString() + '\t' + this.dStartchn.ToString()
               + '\t' + this.dEndchn.ToString() + '\t' + this.dHipx.ToString()
               + '\t' + this.dHipy.ToString() + '\t' + this.dT1.ToString()
               + '\t' + this.dT2.ToString() + '\t' + this.dRadius.ToString()
               + '\t' + this.dEllength.ToString() + '\t' + this.dStartx.ToString()
               + '\t' + this.dStarty.ToString() + '\t' + this.dEndx.ToString()
               + '\t' + this.dEndy.ToString() + '\t' + this.dB1.ToString()
               + '\t' + this.dB2.ToString() + '\t' + this.dDeviation.ToString()
               + '\t' + this.iTurn.ToString();

            return str;
        }

        public static CHalignFilData Parse(string strData)
        {
            string org = strData;

            strData = strData.Trim();

            CHalignFilData FilDataObj = new CHalignFilData();
            string[] Values = strData.Split(new char[] { '\t' });

            if (Values.Length != 20)
            {
                while (strData.IndexOf("  ") != -1)
                {
                    strData = strData.Replace("  ", " ");
                }
                strData = strData.Replace(' ', '\t');
                Values = strData.Split(new char[] { '\t' });
            }
            int iIndex = 0;
            FilDataObj.sMod = Values[iIndex]; iIndex++;	// Read Model Name
            FilDataObj.sString = Values[iIndex]; iIndex++;  	// Read String Name
            FilDataObj.iSlno = int.Parse(Values[iIndex]); iIndex++;// serial no
            FilDataObj.iEltype = short.Parse(Values[iIndex]); iIndex++; // Element type
            FilDataObj.dStartchn = double.Parse(Values[iIndex]); iIndex++;  	// once only           
            FilDataObj.dEndchn = double.Parse(Values[iIndex]); iIndex++; // End chain age 
            FilDataObj.dHipx = double.Parse(Values[iIndex]); iIndex++; // HIP_X, discard
            FilDataObj.dHipy = double.Parse(Values[iIndex]); iIndex++; // HIP_Y, discard
            FilDataObj.dT1 = double.Parse(Values[iIndex]); iIndex++; // T1, discard
            FilDataObj.dT2 = double.Parse(Values[iIndex]); iIndex++; // T2, discard
            FilDataObj.dRadius = double.Parse(Values[iIndex]); iIndex++;  // at times only, set 0 for L1           
            FilDataObj.dEllength = double.Parse(Values[iIndex]); iIndex++; // Length of element, discard
            FilDataObj.dStartx = double.Parse(Values[iIndex]); iIndex++;  // Start x, set for L1 only
            FilDataObj.dStarty = double.Parse(Values[iIndex]); iIndex++; // Start y, set for L1 only
            FilDataObj.dEndx = double.Parse(Values[iIndex]); iIndex++;  // End x
            FilDataObj.dEndy = double.Parse(Values[iIndex]); iIndex++;  // End y 
            FilDataObj.dB1 = double.Parse(Values[iIndex]); iIndex++; // B1
            FilDataObj.dB2 = double.Parse(Values[iIndex]); iIndex++; // B2
            FilDataObj.dDeviation = double.Parse(Values[iIndex]); iIndex++;  // Deviation
            FilDataObj.iTurn = short.Parse(Values[iIndex]); iIndex++;

            return FilDataObj;
        }
    };

    public class CHIPInfo
    {
        public CHIPInfo()
        {

        }
        public bool IsHipData
        {
            get
            {
                bool bHipData = false;
                if (DataList.Count > 0)
                {
                    if (DataList[0].dHipx != 0 || DataList[0].dHipy != 0)
                    {
                        bHipData = true;
                    }
                }
                return bHipData;
            }            
        }
        public string ModelName = "";
        public string StringLabel = "";
        public List<CHalignFilData> DataList = new List<CHalignFilData>();
    };

    public class Eldtype 
    {
        public Eldtype()
        {
        }
	    public short Code = 0;
    }	

    public class DrgReturn 
    {
        public DrgReturn()
        {
        }
        
	    public string		Key;//[30]
	    public double		Value;
    }

    public class CValignFilData
    {
        public string modnam;
        public string stglbl;
        public int elno;
        public int eltype;
        public double chn1;
        public double chn2;
        public double hipx;
        public double hipy;
        public double t1;
        public double t2;
        public double rad;
        public double l;
        public double xs;
        public double ys;
        public double xe;
        public double ye;
        public double b1;
        public double b2;
        public double del;
        public int turn;

        private const char separator = '\t';

        public CValignFilData()
        {

        }

        public override string ToString()
        {
            string strTemp = modnam + separator
                + stglbl + separator
                + elno.ToString() + separator
                + eltype.ToString() + separator
                + chn1.ToString() + separator
                + chn2.ToString() + separator
                + hipx.ToString() + separator
                + hipy.ToString() + separator
                + t1.ToString() + separator
                + t2.ToString() + separator
                + rad.ToString() + separator
                + l.ToString() + separator
                + xs.ToString() + separator
                + ys.ToString() + separator
                + xe.ToString() + separator
                + ye.ToString() + separator
                + b1.ToString() + separator
                + b2.ToString() + separator
                + del.ToString() + separator
                + turn.ToString();

            return strTemp;
        }

        public static CValignFilData Parse(string str)
        {
            str = str.Trim();

            CValignFilData data = new CValignFilData();
            string[] Values = str.Split(new char[] { CValignFilData.separator });

            if (Values.Length != 20)
            {
                while (str.IndexOf("  ") != -1)
                {
                    str = str.Replace("  ", " ");
                }
                str = str.Replace(' ', '\t');
                Values = str.Split(new char[] { '\t' });
            }
            int iIndex = 0;
            data.modnam = Values[iIndex]; iIndex++;
            data.stglbl = Values[iIndex]; iIndex++;
            data.elno = int.Parse(Values[iIndex]); iIndex++;
            data.eltype = int.Parse(Values[iIndex]); iIndex++;
            data.chn1 = double.Parse(Values[iIndex]); iIndex++;
            data.chn2 = double.Parse(Values[iIndex]); iIndex++;
            data.hipx = double.Parse(Values[iIndex]); iIndex++;
            data.hipy = double.Parse(Values[iIndex]); iIndex++;
            data.t1 = double.Parse(Values[iIndex]); iIndex++;
            data.t2 = double.Parse(Values[iIndex]); iIndex++;
            data.rad = double.Parse(Values[iIndex]); iIndex++;
            data.l = double.Parse(Values[iIndex]); iIndex++;
            data.xs = double.Parse(Values[iIndex]); iIndex++;
            data.ys = double.Parse(Values[iIndex]); iIndex++;
            data.xe = double.Parse(Values[iIndex]); iIndex++;
            data.ye = double.Parse(Values[iIndex]); iIndex++;
            data.b1 = double.Parse(Values[iIndex]); iIndex++;
            data.b2 = double.Parse(Values[iIndex]); iIndex++;
            data.del = double.Parse(Values[iIndex]); iIndex++;
            data.turn = int.Parse(Values[iIndex]); iIndex++;

            return data;
        }
    };

    public class CValignInfo
    {
        public string ModelName;
        public string StringLabel;
        public CValignInfo()
        {

        }

        public List<CValignFilData> DataList = new List<CValignFilData>();
    };

    public class  OffsetRec
    {
        public OffsetRec()
        {

        }

        public short OffsetType;
        public short AcceptCode;
        public string ModelName;//[30];
        public string StringName;//[20];
        public string OffsetModel;//[30];
        public string OffsetString;//[20];
        public double StartChainage;
        public double EndChainage;
        public double StartHOffset;
        public double EndHOffset;
        public double StartVOffset;
        public double EndVOffset;
        public double StartSuper;
        public double EndSuper;
        public void ToStream(BinaryWriter bw)
        {
            bw.Write(this.OffsetType);
            bw.Write(this.AcceptCode);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.ModelName, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.StringName, 20));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.OffsetModel, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.OffsetString, 20));
            bw.Write(this.StartChainage);
            bw.Write(this.EndChainage);
            bw.Write(this.StartHOffset);
            bw.Write(this.EndHOffset);
            bw.Write(this.StartVOffset);
            bw.Write(this.EndVOffset);
            bw.Write(this.StartSuper);
            bw.Write(this.EndSuper);            
        }

        public static OffsetRec FromStream(BinaryReader br)
        {
            OffsetRec obj = new OffsetRec();

            obj.OffsetType = br.ReadInt16();
            obj.AcceptCode = br.ReadInt16();
            obj.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));//[30];
            obj.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));//[20];
            obj.OffsetModel = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));//[30];
            obj.OffsetString = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));//[20];
            obj.StartChainage = br.ReadDouble();
            obj.EndChainage = br.ReadDouble();
            obj.StartHOffset = br.ReadDouble();
            obj.EndHOffset = br.ReadDouble();
            obj.StartVOffset = br.ReadDouble();
            obj.EndVOffset = br.ReadDouble();
            obj.StartSuper = br.ReadDouble();
            obj.EndSuper = br.ReadDouble();

            return obj;
        }
    }	    

}
