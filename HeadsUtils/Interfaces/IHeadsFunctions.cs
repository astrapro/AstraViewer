using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace HeadsUtils.Interfaces
{
    public interface IHeadsFunctions
    {
        #region WORKING DIRECTORY
        IWorkingDirSelector CreateWkDirDialog(IHeadsApplication app);        
        #endregion

        #region DRAWSTRING
        System.Windows.Forms.Form CreateDrawStringDialog(IHeadsApplication app);       
        #endregion

        #region HALIGNMENT
        System.Windows.Forms.Form CreateNewHalignHIPMethodDialog(IHeadsApplication app);     

        System.Windows.Forms.Form CreateNewHalignElementMethodDialog(IHeadsApplication app);        

        System.Windows.Forms.Form CreateOpenHIPMethodDialog(IHeadsApplication app, CHIPInfo info);        

        IHalignFilSelector CreateHIPMethodSelectorDialog(IHeadsApplication app);
       
        IHalignFilSelector CreateElementMethodSelectorDialog(IHeadsApplication app);        

        System.Windows.Forms.Form CreateOpenElementMethodDialog(IHeadsApplication app, CHIPInfo info);
        #endregion

        #region L-SECTION
        System.Windows.Forms.Form CreateLSectionDialog(IHeadsApplication app);

        System.Windows.Forms.Form CreateLSecGridDialog(IHeadsApplication app);
        void DeleteLSecGrid(IHeadsApplication app);
        #endregion

        #region CONFIG PARAM
        System.Windows.Forms.Form CreateConfigParamDialog(IHeadsApplication app);
        #endregion

        #region CHAINAGE
        System.Windows.Forms.Form CreateChainageDialog(IHeadsApplication app);

        void DeleteChainage(IHeadsApplication app);
        #endregion

        #region DETAILS
        System.Windows.Forms.Form CreateDetailsDialog(IHeadsApplication app);

        void DeleteDetails(IHeadsApplication app);

        System.Windows.Forms.Form CreateVaricalDetailsDialog(IHeadsApplication app);
        #endregion

        #region COORDINATE
        System.Windows.Forms.Form CreateCoordinateDialog(IHeadsApplication app);

        void DeleteCoordinates(IHeadsApplication app);
        #endregion

        #region VALIGNMENT
        System.Windows.Forms.Form CreateNewValignDialog(IHeadsApplication app);

        IValignFilSelector CreateValignSelectorDialog(IHeadsApplication app);

        System.Windows.Forms.Form CreateOpenValignmentDialog(IHeadsApplication app, CValignInfo info);
        #endregion

        #region MODELLING
        System.Windows.Forms.Form CreateModellingHalignDialog(IHeadsApplication app);

        System.Windows.Forms.Form CreateModellingValignDialog(IHeadsApplication app);

        System.Windows.Forms.Form CreateModellingOffsetDialog(IHeadsApplication app);
        #endregion

        #region BOUNDARY
        System.Windows.Forms.Form CreateBoundaryDialog(IHeadsApplication app, bool bMakeString);
        #endregion

        #region LAYOUT
        void ApplyLayout(IHeadsApplication app);
        

        void AcceptLayout(IHeadsApplication app);       
        #endregion

        #region OFFSET
        System.Windows.Forms.Form CreateOffsetModelDialog(IHeadsApplication app);
        #endregion

        #region DRG FILE
        void ExportToDrgFile(IHeadsApplication app, string strFilePath);
        
        void ImportFromDrgFile(IHeadsApplication app, string strFilePath);        
        #endregion

        #region LOAD DEFLECTION
        System.Windows.Forms.Form CreateLoadDeflectionDialog(IHeadsApplication app);
        #endregion



        #region FIND OPTION
        System.Windows.Forms.Form CreateFindOptionDialog(IHeadsApplication app);
        #endregion
    }
}
