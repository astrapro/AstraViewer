using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmSeismicLoad : Form
    {
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public frmSeismicLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmSeismicLoad_Load(object sender, EventArgs e)
        {

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (r == -1)
                    {
                        if (mlist.StringList[i] == "FX" || mlist.StringList[i] == "FY" || mlist.StringList[i] == "FZ" ||
                            mlist.StringList[i] == "MX" || mlist.StringList[i] == "MY" || mlist.StringList[i] == "MZ")
                        {
                            r = i;
                        }
                    }

                    if (mlist.StringList[i] == "FX")
                    {
                        rbtn_fx_positive.Checked = true;
                    }
                    else if (mlist.StringList[i] == "FZ")
                    {
                        rbtn_fz_positive.Checked = true;
                    }
                }

                if (r != -1)
                {
                    txt_sc.Text = mlist.GetString(1, r - 1).Trim().TrimEnd().TrimStart();
                }
                btn_add.Text = "Change";
                ASTRA_Data.Clear();
            }
            //else
            //    txt_joint_number.Text = iACad.GetSelectedJointsInText();
        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {
            string kStr = "SC ";

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_sc.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + dval.ToString("f3");
            }

            if (rbtn_fx_positive.Checked)
            {
                kStr = kStr + " FX ";
            }

            if (false)
            {
                #region CC

                List<double> list_x = new List<double>();
                List<double> list_y = new List<double>();
                List<double> list_z = new List<double>();


                int i = 0;


                foreach (var item in iACad.AstraDocument.Joints)
                {
                    if (!list_x.Contains(item.X)) list_x.Add(item.X);
                    if (!list_y.Contains(item.Y)) list_y.Add(item.Y);
                    if (!list_z.Contains(item.Z)) list_z.Add(item.Z);
                }


                list_x.Sort();
                list_y.Sort();
                list_z.Sort();

                i = 0;

                System.Collections.Hashtable hash_XY = new System.Collections.Hashtable();
                System.Collections.Hashtable hash_XZ = new System.Collections.Hashtable();
                System.Collections.Hashtable hash_YX = new System.Collections.Hashtable();
                System.Collections.Hashtable hash_YZ = new System.Collections.Hashtable();
                System.Collections.Hashtable hash_ZX = new System.Collections.Hashtable();
                System.Collections.Hashtable hash_ZY = new System.Collections.Hashtable();

                //List<double> coords1 = new List<double>();
                //List<double> coords2 = new List<double>();



                JointCoordinateCollection coords1 = new JointCoordinateCollection();
                JointCoordinateCollection coords2 = new JointCoordinateCollection();




                foreach (var x in list_x)
                {
                    coords1 = new JointCoordinateCollection();
                    coords2 = new JointCoordinateCollection();

                    foreach (var item in iACad.AstraDocument.Joints)
                    {
                        if (item.X == x)
                        {
                            if (!coords1.Contains(item))
                                coords1.Add(item);
                            if (!coords2.Contains(item))
                                coords2.Add(item);
                        }
                    }
                    hash_XY.Add(x, coords1);
                    hash_XZ.Add(x, coords2);
                }




                foreach (var y in list_y)
                {
                    coords1 = new JointCoordinateCollection();
                    coords2 = new JointCoordinateCollection();

                    foreach (var item in iACad.AstraDocument.Joints)
                    {
                        if (item.Y == y)
                        {
                            if (!coords1.Contains(item))
                                coords1.Add(item);
                            if (!coords2.Contains(item))
                                coords2.Add(item);
                        }
                    }
                    hash_YX.Add(y, coords1);
                    hash_YZ.Add(y, coords2);
                }

                hash_ZX = new System.Collections.Hashtable();
                hash_ZY = new System.Collections.Hashtable();

                foreach (var z in list_z)
                {
                    coords1 = new JointCoordinateCollection();
                    coords2 = new JointCoordinateCollection();

                    foreach (var item in iACad.AstraDocument.Joints)
                    {
                        if (item.Z == z)
                        {
                            if (!coords1.Contains(item))
                                coords1.Add(item);
                            if (!coords2.Contains(item))
                                coords2.Add(item);
                        }
                    }
                    hash_ZX.Add(z, coords1);
                    hash_ZY.Add(z, coords2);
                }

                #endregion CC
            }


            Seismic_Calculations ss = new Seismic_Calculations(iACad);
            ss.Set_AS();


            List<double> forces = ss.Get_All_Forces();
            List<double> latforces = ss.Get_Latetal_Forces();

            double W = 0.0;


            foreach (var item in forces)
            {
                W += item;
            }
            double Fs = 0.0;

            foreach (var item in latforces)
            {
                Fs += item;
            }



            double Vb = W * dval;



            List<double> lst_Q = new List<double>();


            foreach (var item in latforces)
            {
                lst_Q.Add(item * Vb / Fs);
            }



            //List<int> ll = ss.Get_FX_Joints(false);


            //ll = ss.Get_FX_Joints(true);

            //ll = ss.Get_FZ_Joints(false);

            //ll = ss.Get_FZ_Joints(true);


            for (int i = 1; i < ss.list_y.Count; i++)
            {
                var jntLst = ss.Get_Floor_Joints(ss.list_y[i]);

                foreach (var item in jntLst)
                {
                    if (rbtn_fz_positive.Checked)
                    {
                        kStr = item + " FZ " + (lst_Q[i - 1] / jntLst.Count ).ToString("f3");
                    }
                    else if (rbtn_fz_negative.Checked)
                    {
                        kStr = item + " FZ -" + (lst_Q[i - 1] / jntLst.Count).ToString("f3");
                    }
                    else if (rbtn_fx_positive.Checked)
                    {
                        kStr = item + " FX " + (lst_Q[i - 1] / jntLst.Count).ToString("f3");
                    }
                    else if (rbtn_fx_negative.Checked)
                    {
                        kStr = item + " FX -" + (lst_Q[i - 1] / jntLst.Count).ToString("f3");
                    }
                    ASTRA_Data.Add(kStr);
                }
            }




            //if (rbtn_fz_positive.Checked)
            //{
            //    kStr = kStr + " FZ ";
            //}

            ASTRA_Data.Add(kStr);

            if (Node != null)
            {
                if (btn_add.Text == "Change")
                {

                  //Node = Node.Parent;
                       
                  //Node.Parent.Nodes.Clear();

                    Node.Text = "JOINT LOAD";

                    //var nd = Node.Nodes.Add("JOINT LOAD");

                    foreach (var item in ASTRA_Data)
                    {
                        Node.Nodes.Add(item);

                    }
                    //nd.Nodes.Add(
                    this.Close();
                }
                else
                {

                    //Node.Text = "JOINT LOAD";

                    var nd = Node.Nodes.Add("JOINT LOAD");

                    foreach (var item in ASTRA_Data)
                    {
                        nd.Nodes.Add(item);

                    }


                    //Node.Nodes.Add(kStr);
                    //Node.Expand();
                    Node.ExpandAll();
                }
            }
            //else
            //{
            this.Close();
            //}
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sc_cal_Click(object sender, EventArgs e)
        {
            string ex_fName = Path.Combine(Application.StartupPath, @"DESIGN\Siesmic Coefficient\Calculation for Siesmic Coefficient.xls");

            if (File.Exists(ex_fName))
            {
                System.Diagnostics.Process.Start(ex_fName);
            }
            else
            {
                MessageBox.Show("Seismic Coefficient help file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }


    }
    public class Seismic_Calculations
    {

        public List<double> list_x { get; set; }
        public List<double> list_y { get; set; }
        public List<double> list_z { get; set; }

        IASTRACAD iACad = null;



        Hashtable hash_XY { get; set; }
        Hashtable hash_XZ { get; set; }
        Hashtable hash_YX { get; set; }
        Hashtable hash_YZ { get; set; }
        Hashtable hash_ZX { get; set; }
        Hashtable hash_ZY { get; set; }


        public Seismic_Calculations(IASTRACAD ACad)
        {
            iACad = ACad;
            list_x = new List<double>();
            list_y = new List<double>();
            list_z = new List<double>();

            hash_XY = new Hashtable();
            hash_XZ = new Hashtable();
            hash_YX = new Hashtable();
            hash_YZ = new Hashtable();
            hash_ZX = new Hashtable();
            hash_ZY = new Hashtable();
            Set_AS();
        }

        public void Set_AS()
        {
            int i = 0;
            list_x.Clear();
            list_y.Clear();
            list_z.Clear();

            hash_XY.Clear();
            hash_XZ.Clear();
            hash_YX.Clear();
            hash_YZ.Clear();
            hash_ZX.Clear();
            hash_ZY.Clear();

            foreach (var item in iACad.AstraDocument.Joints)
            {
                if (!list_x.Contains(item.X)) list_x.Add(item.X);
                if (!list_y.Contains(item.Y)) list_y.Add(item.Y);
                if (!list_z.Contains(item.Z)) list_z.Add(item.Z);
            }

            list_x.Sort();
            list_y.Sort();
            list_z.Sort();

            i = 0;


            JointCoordinateCollection coords1 = new JointCoordinateCollection();
            JointCoordinateCollection coords2 = new JointCoordinateCollection();

            foreach (var x in list_x)
            {
                coords1 = new JointCoordinateCollection();
                coords2 = new JointCoordinateCollection();

                foreach (var item in iACad.AstraDocument.Joints)
                {
                    if (item.X == x)
                    {
                        if (!coords1.Contains(item))
                            coords1.Add(item);
                        if (!coords2.Contains(item))
                            coords2.Add(item);
                    }
                }
                hash_XY.Add(x, coords1);
                hash_XZ.Add(x, coords2);
            }

            foreach (var y in list_y)
            {
                coords1 = new JointCoordinateCollection();
                coords2 = new JointCoordinateCollection();

                foreach (var item in iACad.AstraDocument.Joints)
                {
                    if (item.Y == y)
                    {
                        if (!coords1.Contains(item))
                            coords1.Add(item);
                        if (!coords2.Contains(item))
                            coords2.Add(item);
                    }
                }
                hash_YX.Add(y, coords1);
                hash_YZ.Add(y, coords2);
            }

            hash_ZX = new Hashtable();
            hash_ZY = new Hashtable();

            foreach (var z in list_z)
            {
                coords1 = new JointCoordinateCollection();
                coords2 = new JointCoordinateCollection();

                foreach (var item in iACad.AstraDocument.Joints)
                {
                    if (item.Z == z)
                    {
                        if (!coords1.Contains(item))
                            coords1.Add(item);
                        if (!coords2.Contains(item))
                            coords2.Add(item);
                    }
                }
                hash_ZX.Add(z, coords1);
                hash_ZY.Add(z, coords2);
            }
        }

        public List<int> Get_FX_Joints(bool IsPositive)
        {
            List<int> list = new List<int>();
            double xx = 0.0;


            //JointCoordinateCollection coords;

            List<double> min_x = new List<double>();

            foreach (var z in list_z)
            {
                var coords = hash_ZX[z] as JointCoordinateCollection;
                xx = 0.0;

                foreach (var crds in coords)
                {
                    if (IsPositive) //Min x
                    {
                        if (xx >= crds.X)
                        {
                            xx = crds.X;
                        }
                    }
                    else
                    {
                        if (xx <= crds.X)
                        {
                            xx = crds.X;
                        }
                    }
                }
                min_x.Add(xx);
                
            }

            //foreach (var z in list_z)
            //{
            //    var coords = hash_ZX[z] as JointCoordinateCollection;

            //    foreach (var crds in coords)
            //    {
            //        if (min_x.Contains(crds.X))
            //        {
            //            list.Add(crds.NodeNo);
            //        }
            //    }
            //}

            for (int i = 0; i < list_z.Count; i++)
            {
                var z = list_z[i];

                var coords = hash_ZX[z] as JointCoordinateCollection;

                foreach (var crds in coords)
                {
                    if (min_x[i] == (crds.X))
                    {
                        list.Add(crds.NodeNo);
                    }
                }
                
            }
            list.Sort();
            return list;

        }

        public List<int> Get_FZ_Joints(bool IsPositive)
        {
            List<int> list = new List<int>();
            double zz = 0.0;


            //JointCoordinateCollection coords;

            List<double> min_z = new List<double>();

            foreach (var x in list_x)
            {
                var coords = hash_XZ[x] as JointCoordinateCollection;
                zz = 0.0;

                foreach (var crds in coords)
                {
                    if (!IsPositive) //Min x
                    {
                        if (zz >= crds.Z)
                        {
                            zz = crds.Z;
                        }
                    }
                    else  //Max x
                    {
                        if (zz <= crds.Z)
                        {
                            zz = crds.Z;
                        }
                    }
                }
                min_z.Add(zz);

            }

            //foreach (var x in list_x)
            //{
            //    var coords = hash_XZ[x] as JointCoordinateCollection;

            //    foreach (var crds in coords)
            //    {
            //        if (min_z.Contains(crds.Z))
            //        {
            //            list.Add(crds.NodeNo);
            //        }
            //    }
            //}


            for (int i = 0; i < list_x.Count; i++)
            {
                var z = list_x[i];

                var coords = hash_XZ[z] as JointCoordinateCollection;

                foreach (var crds in coords)
                {
                    if (min_z[i] == (crds.Z))
                    {
                        list.Add(crds.NodeNo);
                    }
                }

            }

            list.Sort();
            return list;

        }

        public List<int> Get_Floor_Joints(double Floor_Level)
        {
            List<int> list = new List<int>();

            var coords = hash_YX[Floor_Level] as JointCoordinateCollection;

            foreach (var crds in coords)
            {
                list.Add(crds.NodeNo);
            }

            list.Sort();
            return list;

        }

        public double Get_Weight(double fromLevel, double toLevel)
        {
            //MemberIncidenceCollection mic = new MemberIncidenceCollection();

            //double v1 = 0.0;
            //double v2 = 21.5;
            double v1 = fromLevel;
            double v2 = toLevel;

            double total_weight = 0.0;

            for (int i = 0; i < iACad.AstraDocument.Members.Count; i++)
            {
                var item = iACad.AstraDocument.Members[i];
                if (item.StartNode.Y >= v1 && item.StartNode.Y <= v2 &&
                    item.EndNode.Y >= v1 && item.EndNode.Y <= v2)
                {
                    //mic.Add(item);
                    if(item.Property.Area == 0.0)
                        total_weight += item.Property.YD * item.Property.ZD * item.Length * 25.0;
                    else
                        total_weight += item.Property.Area * item.Length * 25.0;
                }
            }

            return total_weight;
        }

        public List<double> Get_All_Forces()
        {
            List<double> forces = new List<double>();

            for (int i = 1; i < list_y.Count; i++)
            {

                forces.Add(Get_Weight(list_y[i - 1], list_y[i]));
            }
            return forces;
        }

        public List<double> Get_Latetal_Forces()
        {
            List<double> forces = Get_All_Forces();

            List<double> lat_forces = new List<double>();

            double dval = 0.0;
            for (int i = 1; i < list_y.Count; i++)
            {
                dval = 0.0;
                for (int j = forces.Count - 1; j >= i-1; j--)
                {
                    dval += forces[j];
                }
                //lat_forces.Add(forces[i] * list_y[i] * list_y[i]);
                lat_forces.Add(dval * list_y[i] * list_y[i]);
            }
            return lat_forces;
        }
    }


}
