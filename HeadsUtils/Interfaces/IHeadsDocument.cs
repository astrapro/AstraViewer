using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;

namespace HeadsUtils.Interfaces
{
    public interface IHeadsDocument
    {
        double ActiveTextHeight { get; }
        void DisableUndoRecording(bool bDisable);
        IHdLayer GetLayer(string strName);
        IHdLayer AddLayer(string strName);
        IHdLayer GetActiveLayer();
        IHdPoint DrawPoint(CPoint3D pt);
        IHdLine DrawLine(CPoint3D ptFrom, CPoint3D ptTo);
        IHdArc DrawArc(CPoint3D ptCenter, double radius, double startAngle, double endAngle);
        IHdText DrawText(CPoint3D pt, string strText, double height);
        IHdEllipse DrawEllipse(CPoint3D center, double dRadius);
        IHdPolyline3D DrawPolyline3D(List<CPoint3D> ptList);
        IHd3DFace Draw3DFace(CPoint3D pt1, CPoint3D pt2, CPoint3D pt3, CPoint3D pt4);
        IHdEntity[] GetSelectionSet();
        IHdEntity GetObjectById(int iID);
        void RefreshDocument();
        void RefreshDocument(bool bInvalidate);
        IHdEntity[] GetUserSelection(string strPrompt);
        CPoint3D GetUserPoint(string strPrompt);
        void SetPrompt(string strPrompt);
        IHdEntity[] Entities { get; }
        CBox BoundingBox { get; }
        CCfgtype ConfigParam { get; set; }
        void RemoveAllEntities();


        int Search_Text(string search_str);
        int Search_Text(string search_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text);

        int Replace_Text(string search_str, string replace_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text);
        int Replace_All(string search_str, string replace_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text);

        object Search_Entity(string search_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text);

        void Stop_Blink();

    
    }
}
