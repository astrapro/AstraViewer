using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_ASTRA_Controls : Form
    {
        IASTRA_Control iASTRA { get; set; }
        public frm_ASTRA_Controls(IASTRA_Control iASTRA)
        {
            InitializeComponent();
            this.iASTRA = iASTRA;
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;
            //MessageBox.Show(tab.SelectedTab.Name);
                    
            if (tab.SelectedTab.Name == tab_geometry.Name)
            {
                iASTRA.DrawStructure();
            }
            else if (tab.SelectedTab.Name == tab_loading.Name)
            {
                iASTRA.ASTRA_CommandExecute("LoadingOn");
            }
            else if (tab.SelectedTab.Name == tab_max_force.Name)
            {
                iASTRA.ASTRA_CommandExecute("MaxForce");
            }
        }
    }
}
public interface IASTRA_Control
{
    void ASTRA_CommandExecute(string commandname);
    void DrawStructure();
}