using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HeadsUtils;
using HeadsUtils.Interfaces;

namespace HeadsFunctions1.LSection
{
    internal partial class FormLSectionGrid : Form
    {
        # region private data
        private IHeadsApplication headsApp;
        # endregion

        # region constructors / destructors
        public FormLSectionGrid(IHeadsApplication app)
        {
            InitializeComponent();

            this.headsApp = app;
            
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Blue);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Cyan);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Green);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Magenta);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Red);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.White);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.Yellow);
            this.comboBoxTextColor.Items.Add(eHEADS_COLOR.DarkGray);

            this.comboBoxTextColor.SelectedItem = eHEADS_COLOR.DarkGray;
        }
        # endregion

        # region Properties
        # endregion

        # region APIs
        public bool DrawGridLines()
        {
            bool bSuccess = false;

            if (this.headsApp.ActiveDocument.ConfigParam.XMetric != 0
                && this.headsApp.ActiveDocument.ConfigParam.YMetric != 0)
            {
                string strGridLabel = HeadsUtils.Constants.LABEL_GRID + ":LSec";
                ViewerUtils.DeleteEntitiesByLabel(this.headsApp.ActiveDocument, strGridLabel, true);
                               
                                
                CBox box = this.headsApp.ActiveDocument.BoundingBox;
                if (box != null)
                {
                    double yhorz = 0;
                    //int iyhorz = 0;

                    double[] xy = new double[] { box.BottomRight.X, box.BottomRight.Y, box.TopLeft.X, box.TopLeft.Y };


                    double m_dchnintv = this.tbChainageInterval_.Value;
                    double m_dlvlintv = this.tbLevelInterval_.Value;
                    double dLSectionXsc = this.headsApp.ActiveDocument.ConfigParam.XMetric;
                    double dLSectionYsc = this.headsApp.ActiveDocument.ConfigParam.YMetric;
                    double m_dtxtsize = this.tbTextSize_.Value;



                    xy[0] = ((int)(xy[0] / dLSectionXsc / m_dchnintv)) * dLSectionXsc * m_dchnintv;
                    xy[2] = ((int)(xy[2] / dLSectionXsc / m_dchnintv + 0.5)) * dLSectionXsc * m_dchnintv;
                    xy[1] = ((int)(xy[1] / dLSectionYsc / m_dlvlintv)) * dLSectionYsc * m_dlvlintv;
                    xy[3] = ((int)(xy[3] / dLSectionYsc / m_dlvlintv + 0.5)) * dLSectionYsc * m_dlvlintv;


                    int xglines = 0, yglines = 0;

                    xglines = (int)(Math.Abs(xy[0] - xy[2]) / m_dchnintv / dLSectionXsc);
                    xglines += 1;
                    // therefore x right limit = m_dchnintv*xglines
                    double xl = xy[0] + m_dchnintv * xglines; // use instead of xy[2]

                    yglines = (int)(Math.Abs(xy[1] - xy[3]) / m_dlvlintv / dLSectionYsc);
                    yglines += 1;
                    // therefore y top limit = m_dlvlintv*yglines
                    double yl = xy[1] + m_dlvlintv * yglines * dLSectionYsc; // use instead of xy[3]

                    IHdLine aLine;
                    IHdText aText;

                    CPoint3D pt = new CPoint3D();
                    CPoint3D pt1 = new CPoint3D();
                    CPoint3D ptxt = new CPoint3D();
                    double[] res = new double[2];
                    string sTxt = string.Empty;

                    // now x grid lines VERTICAL
                    for (int i = 0; i <= xglines; i++)
                    {
                        res[0] = xy[0] + i * m_dchnintv; //x1; 
                        res[1] = xy[1]; //y1;
                        pt = new CPoint3D(res[0], res[1], 0);

                        res[0] = xy[0] + (i - 0.1) * m_dchnintv;  //x1; 
                        res[1] = xy[1] - 1.5 * m_dtxtsize; //y1;
                        ptxt = new CPoint3D(res[0], res[1], 0);


                        res[0] = xy[0] + i * m_dchnintv; //x2;
                        res[1] = yl; //y2;
                        pt1 = new CPoint3D(res[0], res[1], 0);



                        aLine = this.headsApp.ActiveDocument.DrawLine(pt, pt1);

                        double txtValue = (double)((xy[0] + i * m_dchnintv) / dLSectionXsc);
                        sTxt = txtValue.ToString("0");
                        aText = this.headsApp.ActiveDocument.DrawText(ptxt, sTxt, m_dtxtsize);


                        aText.color = (eHEADS_COLOR)this.comboBoxTextColor.SelectedItem;
                        aLine.color = (eHEADS_COLOR)this.comboBoxTextColor.SelectedItem;
                        aText.Rotate(ptxt, -90 * Math.PI / 180.0);

                        aLine.Label = strGridLabel;
                        aText.Label = strGridLabel;
                    }

                    // now y grid lines HORIZONTAL
                    for (int i = 0; i <= yglines; i++)
                    {
                        yhorz = xy[1] + i * m_dlvlintv * dLSectionYsc; //y1;
                        yhorz = (int)yhorz;

                        res[0] = xy[0];  //x1; 
                        res[1] = yhorz;
                        pt = new CPoint3D(res[0], res[1], 0);

                        res[0] = xy[0] - 5 * m_dtxtsize;  //x1; 
                        res[1] = yhorz;
                        ptxt = new CPoint3D(res[0], res[1], 0);


                        res[0] = xl;  //x2;
                        res[1] = yhorz;

                        pt1 = new CPoint3D(res[0], res[1], 0);

                        aLine = this.headsApp.ActiveDocument.DrawLine(pt, pt1);

                        double txtValue = (double)(xy[1] + i * m_dlvlintv);
                        sTxt = txtValue.ToString("0");
                        aText = this.headsApp.ActiveDocument.DrawText(ptxt, sTxt, m_dtxtsize);

                        aText.color = (eHEADS_COLOR)this.comboBoxTextColor.SelectedItem;
                        aLine.color = (eHEADS_COLOR)this.comboBoxTextColor.SelectedItem;

                        aLine.Label = strGridLabel;
                        aText.Label = strGridLabel;
                    }
                    this.headsApp.ActiveDocument.RefreshDocument();
                    this.headsApp.ZoomExtents();

                    bSuccess = true;
                }
            }

            return bSuccess;
            
        }
        # endregion

        # region private utility methods
        private void UpdateColorPanel(eHEADS_COLOR color)
        {
            this.panelColor_.BackColor = Color.FromName(color.ToString());
        }
        # endregion

        private void comboBoxTextColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateColorPanel((eHEADS_COLOR)comboBoxTextColor.SelectedItem);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DrawGridLines();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}