using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using HEADSNeed.ASTRA.ASTRAClasses;
using MovingLoadAnalysis.DataStructure;
using MovingLoadAnalysis;

using System.Windows.Forms;


namespace HEADSNeed.ASTRA.ASTRAClasses.StructureDesign
{
    public class BillOfQuantity
    {
        //ASTRADoc AST_Doc { get; set; }
        StructureMemberAnalysis StructureAnalysis { get; set; }

        public ASTRADoc AST_DOC { get; set; }

        public string Report_File { get; set; }

        public Rebar_Weights Rebars { get; set; }

        #region SLAB BOQ
        public Hashtable Table_BOQ_Slab { get; set; }

        public DataGridView DGV_SLAB_CONC { get; set; }
        public DataGridView DGV_SLAB_STEEL { get; set; }
        #endregion SLAB BOQ

        #region BEAM BOQ
        public Hashtable Table_BOQ_Beam { get; set; }

        public DataGridView DGV_BEAM_CONC { get; set; }
        public DataGridView DGV_BEAM_STEEL { get; set; }
        #endregion BEAM BOQ

        #region COLUMN BOQ
        public Hashtable Table_BOQ_Column { get; set; }

        public DataGridView DGV_COLUMN_CONC { get; set; }
        public DataGridView DGV_COLUMN_STEEL { get; set; }
        #endregion COLUMN BOQ

        #region STAIRCASE BOQ
        public Hashtable Table_BOQ_Staircase { get; set; }

        public DataGridView DGV_STAIRCASE_CONC { get; set; }
        public DataGridView DGV_STAIRCASE_STEEL { get; set; }
        #endregion STAIRCASE BOQ


        #region ISOLATED Footing Foundation BOQ
        public Hashtable Table_BOQ_ISO_Foundation { get; set; }

        public DataGridView DGV_ISO_FOUNDATION_CONC { get; set; }
        public DataGridView DGV_ISO_FOUNDATION_STEEL { get; set; }
        #endregion ISOLATED Footing FOUNDATION BOQ

        #region PILE FOUNDATION BOQ
        public Hashtable Table_BOQ_Pile_Foundation { get; set; }

        public DataGridView DGV_PILE_FOUNDATION_CONC { get; set; }
        public DataGridView DGV_PILE_FOUNDATION_STEEL { get; set; }
        #endregion PILE FOUNDATION BOQ

        public BillOfQuantity()
        {
            Report_File = "";

            //this.StructureAnalysis = StructureAnalysis;

            Table_BOQ_Slab = new Hashtable();
            Table_BOQ_Beam = new Hashtable();
            Table_BOQ_Column = new Hashtable();
            Table_BOQ_Staircase = new Hashtable();
            Table_BOQ_ISO_Foundation = new Hashtable();
            Table_BOQ_Pile_Foundation = new Hashtable();

            Rebars = Tables.Get_Rebar_Weights();
        }

        public eDesignStandard DesignStandard { get; set; }

        public void Calculate_Program()
        {

            List<string> list = new List<string>();



            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*       ITEM WISE BILL OF QUANTITIES         *");
            list.Add("\t\t*      ESTIMATION OF TAKE OFF QUANTITY       *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");

            #endregion

            #region Column
            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: COLUMN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Column Concrete"));
            list.Add(string.Format(""));
            list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            list.Add(string.Format("                                        From    To                                     Area x Length"));
            list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            list.Add(string.Format(" "));

            int i = 1;

            
            foreach (var item in Table_BOQ_Column.Values)
            {
                Column_BOQ cboq = item as Column_BOQ;
                list.Add(string.Format("{0}", cboq.ToString()));
            }
            list.Add(string.Format("                                                      ------------------------------------"));
            list.Add(string.Format("                                                      Total Concrete (Cu.m)"));
            list.Add(string.Format(""));












            list.Add(string.Format("Column Steel Reinforcement"));
            list.Add(string.Format(""));
            list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            foreach (var item in Table_BOQ_Column.Values)
            {
                Column_BOQ cboq = item as Column_BOQ;

                foreach (var sr in cboq.Steel_Reinforcement)
                {
                    list.Add(string.Format("{0}", sr.ToString()));

                }
            }
            list.Add(string.Format("2."));
            list.Add(string.Format("                                                      ------------------------------------"));
            list.Add(string.Format("                                                      Total Steel (M.TON)"));
            list.Add(string.Format(""));
            list.Add(string.Format("============================================================================================================"));

            #endregion Column



            #region Column
            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: BEAM"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Beam Concrete"));
            list.Add(string.Format(""));
            list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            list.Add(string.Format("                                        From    To                                     Area x Length"));
            list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            list.Add(string.Format(" "));

            //int i = 1;


            foreach (var item in Table_BOQ_Column.Values)
            {
                Column_BOQ cboq = item as Column_BOQ;
                list.Add(string.Format("{0}", cboq.ToString()));
            }
            list.Add(string.Format("                                                      ------------------------------------"));
            list.Add(string.Format("                                                      Total Concrete (Cu.m)"));
            list.Add(string.Format(""));












            list.Add(string.Format("Beam Steel Reinforcement"));
            list.Add(string.Format(""));
            list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            foreach (var item in Table_BOQ_Column.Values)
            {
                Column_BOQ cboq = item as Column_BOQ;

                foreach (var sr in cboq.Steel_Reinforcement)
                {
                    list.Add(string.Format("{0}", sr.ToString()));

                }
            }
            list.Add(string.Format("2."));
            list.Add(string.Format("                                                      ------------------------------------"));
            list.Add(string.Format("                                                      Total Steel (M.TON)"));
            list.Add(string.Format(""));
            list.Add(string.Format("============================================================================================================"));

            #endregion Column

            if (Report_File != null)
            {
                File.WriteAllLines(Report_File, list.ToArray());
            }

        }

        public List<string> Get_Slab_BOQ()
        {
            List<string> list = new List<string>();

            #region Slab

            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: SLAB"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Slab Concrete Quantity"));
            list.Add(string.Format("--------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            //list.Add(string.Format("                                        From    To                                     Area x Length"));
            //list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            //list.Add(string.Format(" "));

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No.   Beam Nos.                               Section         Floor        Slab        Floor Area       Quantity"));
            list.Add(string.Format("                                                              Elevation    Thickness                   Area x Thickness "));
            list.Add(string.Format("                                                                 (m)         (mm)         (Sq.m)          (Cu.m)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));


            int i = 1;

            double res, total;

            res = 0;
            total = 0;
            
            Slab_BOQ boq;

            foreach (var item in Table_BOQ_Slab.Values)
            {
                boq = item as Slab_BOQ;
                list.Add(string.Format(" {0,-6} {1}",i++, boq.ToString()));
                res += boq.Quantity;
            }

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                                                               Total Concrete (Cu.m) = {0,9:f3}", res));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.0 : Slab Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));



            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            //double res, total;

            res = 0;
            total = 0;
            i = 1;
            foreach (var item in Table_BOQ_Slab.Values)
            {
                boq = item as Slab_BOQ;


                res = 0;
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("STEP 2.{0} : Slab Steel Reinforcement for S{0}", i++));
                list.Add(string.Format("---------------------------------------------------"));

                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
                //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
                //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("--------------------------------------------------------------------------------"));
                list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
                list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
                list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
                list.Add(string.Format("--------------------------------------------------------------------------------"));
                list.Add(string.Format(""));

                foreach (var sr in boq.Steel_Reinforcement)
                {
                    list.Add(string.Format("{0}", sr.ToString()));
                    res += sr.Total_Weight;

                }
                total += res;
                list.Add(string.Format("--------------------------------------------------------------------------------"));
                list.Add(string.Format("                                               Total Steel (M.TON) = {0,9:f4}", res));
                list.Add(string.Format("--------------------------------------------------------------------------------"));
                

            }
            list.Add(string.Format("================================================================================"));
            list.Add(string.Format("                                      All Slab Total Steel (M.TON) = {0,9:f4}", total));
            list.Add(string.Format("================================================================================"));
            list.Add(string.Format(""));

            #endregion Slab



            return list;
        }
        public List<string> Get_Beam_BOQ()
        {

            List<string> list = new List<string>();


            #region Beam

            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: BEAM"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Beam Concrete Quantity"));
            list.Add(string.Format("--------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            //list.Add(string.Format("                                        From    To                                     Area x Length"));
            //list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            //list.Add(string.Format(" "));

            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No.   Continuos Member Nos.                   Section         Floor        Beam          Area          Quantity"));
            //list.Add(string.Format("                                                              Elevation     Length                     Area x Length "));
            //list.Add(string.Format("                                                                 (m)          (m)         (Sq.m)          (Cu.m)"));
            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));




            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No.   Continuos Member Nos.                   Floor          Section        Area         Beam           Quantity"));
            //list.Add(string.Format("                                              Elevation                                   Length        Area x Length "));
            //list.Add(string.Format("                                                 (m)                           (m)        (Sq.m)          (Cu.m)"));
            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));


            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No.   Continuos Member Nos.                   Floor          Section        Area          Beam        Quantity"));
            list.Add(string.Format("                                              Elevation                                    Length    Area x Length "));
            list.Add(string.Format("                                                 (m)                           (m)         (Sq.m)       (Cu.m)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            int i = 1;

            double res, total;

            res = 0;
            total = 0;

            Beam_BOQ boq;

            for (i = 0; i < DGV_BEAM_CONC.RowCount; i++)
            {

                boq = Table_BOQ_Beam[DGV_BEAM_CONC[1, i].Value.ToString()] as Beam_BOQ;
                if (boq == null) continue;

                list.Add(string.Format(" {0,-6} {1}", i + 1, boq.ToString()));
                res += boq.Quantity;
            }

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                                                               Total Concrete (Cu.m) = {0,9:f3}", res));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.0 : Beam Steel Reinforcement"));
            list.Add(string.Format("-------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));



            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            //double res, total;

            res = 0;
            total = 0;
            i = 1;
            int sl_incr = 1;
            for (i = 0; i < DGV_BEAM_CONC.RowCount; i++)
            {
                boq = Table_BOQ_Beam[DGV_BEAM_CONC[1, i].Value.ToString()] as Beam_BOQ;
           

                res = 0;
                sl_incr = 1;
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("STEP 2.{0} : Beam Steel Reinforcement for B{0}", i + 1));
                list.Add(string.Format("---------------------------------------------------"));

                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
                //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
                //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));
                //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area               Bar Weight       Total Weight "));
                //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
                //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                                 (TON)                                               "));
                //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));



                list.Add(string.Format(""));
                list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area              Unit Weight       Total Weight "));
                list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
                list.Add(string.Format("                         (mm)       (m)             (Sq.mm)             (Ton/m)             (Ton)                                               "));
                list.Add(string.Format("------------------------------------------------------------------------------------------------------"));

                foreach (var sr in boq.Steel_Reinforcement)
                {
                    sr.S_No = sl_incr++;
                    list.Add(string.Format("{0}", sr.ToString()));
                    res += sr.Total_Weight;

                }
                total += res;
                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                //list.Add(string.Format("                                                      Total Steel (M.TON) = {0,9:f4}", res));
                //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));


                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("{0,90} = {1:f4}", "Total Steel (M. Ton) ", res));
                list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
                list.Add(string.Format(""));

            }
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format("                                             All Slab Total Steel (M.TON) = {0,9:f4}", total));
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format(""));

            #endregion Beam


            return list;
        }

        public List<string> Get_Column_BOQ()
        {

            List<string> list = new List<string>();
            #region Column

            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: COLUMN"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Column Concrete Quantity"));
            list.Add(string.Format("-----------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            //list.Add(string.Format("                                        From    To                                     Area x Length"));
            //list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            //list.Add(string.Format(""));

            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No.   Continuos Member Nos.                   Section       From Floor    To Floor      Column          Area          Quantity"));
            list.Add(string.Format("                                                              Elevation     Elevation     Length                     Area x Length "));
            list.Add(string.Format("                                                                 (m)          (m)          (m)           (Sq.m)          (Cu.m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));


            int i = 1;

            double res, total;

            res = 0;
            total = 0;

            Column_BOQ boq;
            List<string> step2 = new List<string>();
            for (i = 0; i < DGV_COLUMN_CONC.RowCount; i++)
            {
                boq = Table_BOQ_Column[DGV_COLUMN_CONC[1,i].Value.ToString()] as Column_BOQ;
                if (boq != null)
                {
                    list.Add(string.Format(" {0,-6} {1}", i + 1, boq.ToString()));
                    res += boq.Quantity;
                }
            }

            
            //foreach (var item in Table_BOQ_Column.Values)
            //{

            //    boq = item as Column_BOQ;
            //    list.Add(string.Format(" {0,-6} {1}", i++, boq.ToString()));
            //    res += boq.Quantity;
            //}

            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                                                                              Total Concrete (Cu.m) = {0,9:f3}", res));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.0 : Column Steel Reinforcement"));
            list.Add(string.Format("-------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));



            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            //double res, total;

            res = 0;
            total = 0;
            i = 1;
            int sl_incr = 1;


            #region Chiranjit[2015 04 02]
            for (i = 0; i < DGV_COLUMN_CONC.RowCount; i++)
            {
                boq = Table_BOQ_Column[DGV_COLUMN_CONC[1, i].Value.ToString()] as Column_BOQ;
                if (boq == null) continue;



                //boq = item as Column_BOQ;


                res = 0;
                sl_incr = 1;
                list.Add(string.Format(""));
                list.Add(string.Format("---------------------------------------------------"));
                list.Add(string.Format("STEP 2.{0} : Column Steel Reinforcement for C{0}", i + 1));
                list.Add(string.Format("---------------------------------------------------"));

                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
                list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
                list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

                foreach (var sr in boq.Steel_Reinforcement)
                {
                    sr.S_No = sl_incr++;
                    list.Add(string.Format("{0}", sr.ToString()));
                    res += sr.Total_Weight;

                }
                total += res;
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("                                                      Total Steel (M.TON) = {0,9:f4}", res));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            }
            #endregion Chiranjit[2015 04 02]


            #region Chiranjit[2015 04 02]
            //foreach (var item in Table_BOQ_Column.Values)
            //{
            //    boq = item as Column_BOQ;
            //    res = 0;
            //    sl_incr = 1;
            //    list.Add(string.Format(""));
            //    list.Add(string.Format("---------------------------------------------------"));
            //    list.Add(string.Format("STEP 2.{0} : Column Steel Reinforcement for C{0}", i++));
            //    list.Add(string.Format("---------------------------------------------------"));

            //    list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //    list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //    list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //    list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            //    foreach (var sr in boq.Steel_Reinforcement)
            //    {
            //        sr.S_No = sl_incr++;
            //        list.Add(string.Format("{0}", sr.ToString()));
            //        res += sr.Total_Weight;

            //    }
            //    total += res;
            //    list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //    list.Add(string.Format("                                                      Total Steel (M.TON) = {0,9:f4}", res));
            //    list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            //}
            #endregion Chiranjit[2015 04 02]




            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format("                                             All Slab Total Steel (M.TON) = {0,9:f4}", total));
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format(""));

            #endregion Column

            return list;
        }
        public List<string> Get_Staircase_BOQ()
        {

            List<string> list = new List<string>();

            return list;
        }
        public List<string> Get_Footing_BOQ()
        {

            List<string> list = new List<string>();

            #region Isolated Footing Foundation

 //1      8           0.540    7.840        1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800
 //2      10          0.540      7.840      1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800
 //3      26          0.540      7.840      1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800

            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: Isolated Footing Foundation"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Isolated Footing Foundation Concrete Quantity"));
            list.Add(string.Format("--------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            //list.Add(string.Format("                                        From    To                                     Area x Length"));
            //list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            //list.Add(string.Format(""));

            //list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No.   Continuos Member Nos.                   Section       From Floor    To Floor      Column          Area          Quantity"));
            //list.Add(string.Format("                                                              Elevation     Elevation     Length                     Area x Length "));
            //list.Add(string.Format("                                                                 (m)          (m)          (m)           (Sq.m)          (Cu.m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format("Structral Member: Foundation"));
            list.Add(string.Format(""));
            list.Add(string.Format("Foundation Trapezoidal Part Concrete"));
            list.Add(string.Format(""));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Column        Area of Footing    Thickness       Quantity1         Rect. Area    Thickness     Quantity2  Pedestal    Pedestal    Pedestal    Total"));
            list.Add(string.Format("      Member No.     Top    Bottom       h1 (m.)    [(A1+A2)/2]xh1       of Footing                             Area         Height     Quantity3   Quantity "));
            list.Add(string.Format("                     A1       A2                        (Cu.m)               (Sq.m)       m.          (Cu.m)    (Sq.m.)      (m)         (Cu.m)     (Cu.m)"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------"));

            int i = 1;

            double res, total;

            res = 0;
            total = 0;

            ISO_Foundation_BOQ boq;

            foreach (var item in Table_BOQ_ISO_Foundation.Values)
            {
                boq = item as ISO_Foundation_BOQ;
                list.Add(string.Format(" {0,-6} {1}", i++, boq.ToString()));
                res += boq.Total_Foundation_Quantity;
            }

            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                                                                                                         Total Concrete (Cu.m) = {0,9:f3}", res));
            list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.0 : Isolated Footing Foundation Steel Reinforcement"));
            list.Add(string.Format("-----------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));



            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            //double res, total;

            res = 0;
            total = 0;
            i = 1;
            int sl_incr = 1;
            foreach (var item in Table_BOQ_ISO_Foundation.Values)
            {
                boq = item as ISO_Foundation_BOQ;


                res = 0;
                sl_incr = 1;
                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------------------------------------------------------"));
                list.Add(string.Format("STEP 2.{0} : Isolated Footing Foundation Steel Reinforcement for F{0}", i++));
                list.Add(string.Format("-------------------------------------------------------------------------------"));

                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
                list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
                list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

                foreach (var sr in boq.Steel_Reinforcement)
                {
                    sr.S_No = sl_incr++;
                    list.Add(string.Format("{0}", sr.ToString()));
                    res += sr.Total_Weight;

                }
                total += res;
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("                                                      Total Steel (M.TON) = {0,9:f4}", res));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            }
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format("                                             All Slab Total Steel (M.TON) = {0,9:f4}", total));
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format(""));

            #endregion Isolated Footing Foundation

            return list;
        }
        public List<string> Get_Pile_BOQ()
        {

            List<string> list = new List<string>();
            #region Pile Foundation

            //1      8           0.540    7.840        1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800
            //2      10          0.540      7.840      1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800
            //3      26          0.540      7.840      1.200      9.408      7.840      0.300      1.257      0.540      0.250      0.135     10.800

            //1      5           1.000       0.785        25.000          8.000           157.080       18.920         1.600     30.272     187.352
            //2      4           1.000       0.785        25.000          8.000           157.080       18.490         1.500     27.735     184.815

            list.Add(string.Format("============================================================================================================"));
            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: Pile Foundation"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("STEP 1 : Pile Foundation Concrete Quantity"));
            list.Add(string.Format("--------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("S.No.   Member No.   Section            Floor Elevation     Length        Area           Quantity   "));
            //list.Add(string.Format("                                        From    To                                     Area x Length"));
            //list.Add(string.Format("                    (mm x mm)           (m)     (m)         (m)         (Sq.m)            (Cu.m)"));
            //list.Add(string.Format(""));

            //list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No.   Continuos Member Nos.                   Section       From Floor    To Floor      Column          Area          Quantity"));
            //list.Add(string.Format("                                                              Elevation     Elevation     Length                     Area x Length "));
            //list.Add(string.Format("                                                                 (m)          (m)          (m)           (Sq.m)          (Cu.m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format("Structral Member: Pile Foundation"));
            list.Add(string.Format(""));
            //list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Column       Pile Dia.   Pile Area   Length       Pile Nos.   Quantity1       Pile Cap    Pile Cap         Quantity2      Total "));
            //list.Add(string.Format("      Member No.      (m.)      (Sq.m)     of Pile(m.)               (Cu.m)         Area (Sq.m) Thickness(m)      (Cu.m)        Quantity (Cu.m)"));
            //list.Add(string.Format("                      [D]         [A]         [h1]        [N]       [N x A x h1]       [AC]        h2            [N x AC x h2]   QTY1 + QTY2 "));
            //list.Add(string.Format("--------------------------------------------------------------------------------------------------------------------------------------------------"));
            int i = 1;
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Column       Pile Dia.   Pile Area   Length       Pile Nos.   Quantity1       Pile Cap    Pile Cap      Quantity2    Total "));
            list.Add(string.Format("      Member No.      (m.)      (Sq.m)     of Pile(m.)               (Cu.m)         Area (Sq.m) Thickness(m)   (Cu.m)      Quantity (Cu.m)"));
            list.Add(string.Format("                      [D]         [A]         [h1]        [N]       [N x A x h1]       [AC]        h2       [N x AC x h2]  QTY1 + QTY2 "));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            double res, total;

            res = 0;
            total = 0;

            Pile_Foundation_BOQ boq;

            foreach (var item in Table_BOQ_Pile_Foundation.Values)
            {
                boq = item as Pile_Foundation_BOQ;
                list.Add(string.Format(" {0,-6} {1}", i++, boq.ToString()));
                res += boq.Total_Quantity;
            }

            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("                                                                                                  Total Concrete (Cu.m) = {0,9:f3}", res));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));

            list.Add(string.Format("STEP 2.0 : Pile Foundation Steel Reinforcement"));
            list.Add(string.Format("-----------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));



            //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
            //list.Add(string.Format("1.     CAst1       8     16     3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
            //list.Add(string.Format(""));

            //double res, total;




            res = 0;
            total = 0;
            i = 1;
            int sl_incr = 1;
            foreach (var item in Table_BOQ_Pile_Foundation.Values)
            {
                boq = item as Pile_Foundation_BOQ;


                res = 0;
                sl_incr = 1;
                list.Add(string.Format(""));
                list.Add(string.Format("-------------------------------------------------------------------------------"));
                list.Add(string.Format("STEP 2.{0} : Pile Foundation Steel Reinforcement for F{0}", i++));
                list.Add(string.Format("-------------------------------------------------------------------------------"));

                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area            Bar Weight        Total Weight "));
                list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [Bw]        [N x L x 7.9 / 10^6] "));
                list.Add(string.Format("                         (mm)       (m)             (Sq.mm)            (Ton/m)             (TON)                                               "));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

                foreach (var sr in boq.Steel_Reinforcement)
                {
                    sr.S_No = sl_incr++;
                    list.Add(string.Format("{0}", sr.ToString()));
                    res += sr.Total_Weight;

                }
                total += res;
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
                list.Add(string.Format("                                                      Total Steel (M.TON) = {0,9:f4}", res));
                list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            }
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format("                                             All Slab Total Steel (M.TON) = {0,9:f4}", total));
            list.Add(string.Format("======================================================================================================"));
            list.Add(string.Format(""));

            #endregion Isolated Footing Foundation

            return list;
        }

    }

    public class Steel_Reinforcement
    {
        //SLAB Steel Reinforcement
        //S.No. Bar Mark    Nos.   Dia    Length   Total Weight 
        //                         (mm)   (m)      (Ton)
        //1.     SAst1        8     16    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=....

        public int S_No;
        public string BarMark;
        public string Text { get; set; }
        public double Bar_Spacing { get; set; }
        public int Number_Of_Bars { get; set; }
        public double Bar_Dia { get; set; }
        public double Length { get; set; }
        //public double Bar_Weight { get; set; }
        public double Bar_Weight
        {
            get
            {
                return Tables.Rebars.Get_Rebar_Weight(Bar_Dia);
            }
        }

        public double Total_Weight
        {
            get
            {
                if(Bar_Weight == 0)
                    return Number_Of_Bars * (Math.PI * Math.Pow(Bar_Dia / 1000.0, 2) / 4) * Length * 7.9;

                return Number_Of_Bars * Length * Bar_Weight;
            }
        }

        public double Area
        {
            get
            {
                return Number_Of_Bars * (Math.PI * Math.Pow(Bar_Dia, 2) / 4);

            }
        }

       


        public Steel_Reinforcement()
        {
            S_No = 0;
            BarMark = "";
            Text = "";
            Number_Of_Bars = 0;
            Bar_Dia = 0;
            Bar_Spacing = 0;

            Length = 0;
        }

        public override string  ToString()
        {
            //return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10}      {2} x (3.1416 x {5:f3} x {5:f3} / 4) x {4} x 7.9 = {6:f4}",
            //    S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, (Bar_Dia / 1000.0), Total_Weight));





            //Chiranjit [2015 04 09]
            //return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10:f3}  {5,18:f4}   {6,18:f6} {7,17:f4}",
            //               S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, (Math.PI * Math.Pow(Bar_Dia, 2) / 4), Bar_Weight, Total_Weight));



            return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10:f3} {5,18:f6} {6,18:f4}",
                           S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, Bar_Weight, Total_Weight));

            //return (string.Format("{0,-5} {1,-46} {2,5} {3,8} {4,10:f3} {5,18:f6} {6,18:f4}",
            //               S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, Bar_Weight, Total_Weight));


            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                       Total Weight "));
            //list.Add(string.Format("                   N       D         L        A = 3.1416 x (D/1000)^2 / 4      N x A X L x 7.9                  "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.m)                          (TON)                                               "));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));



        }

        public string ToString(string Element)
        {
            //return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10}      {2} x (3.1416 x {5:f3} x {5:f3} / 4) x {4} x 7.9 = {6:f4}",
            //    S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, (Bar_Dia / 1000.0), Total_Weight));





            //Chiranjit [2015 04 09]
            //return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10:f3}  {5,18:f4}   {6,18:f6} {7,17:f4}",
            //               S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, (Math.PI * Math.Pow(Bar_Dia, 2) / 4), Bar_Weight, Total_Weight));



            //return (string.Format("{0,-5} {1,-8} {2,5} {3,8} {4,10:f3} {5,18:f6} {6,18:f4}",
            //               S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, Bar_Weight, Total_Weight));

            return (string.Format("{0,-5} {1,-46} {2,5} {3,8} {4,10:f3} {5,18:f6} {6,18:f4}",
                           S_No, BarMark, Number_Of_Bars, Bar_Dia, Length, Bar_Weight, Total_Weight));


            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                       Total Weight "));
            //list.Add(string.Format("                   N       D         L        A = 3.1416 x (D/1000)^2 / 4      N x A X L x 7.9                  "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.m)                          (TON)                                               "));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));



        }

    }

    public class Slab_BOQ
    {

        #region Inputs
        //Structral Member: SLAB

        //SLAB Concrete

        //S.No.      Beam Nos.  Section 	    Floor Elevation  	Slab Thickness   Floor Area        Quantity   		
        //                                      (mm)	     	 (Sq.m)                               Area x Thickness 
        //                                                                                           (Cu.m)		

        // 1.        			    3.8                 160              104.0             104.0 x 0.160

        // 2.
        //                                                            --------------------------------------------
        //                                    Total Concrete (Cu.m)

        //SLAB Steel Reinforcement

        //S.No. Bar Mark    Nos.   Dia    Length   Total Weight 
        //                         (mm)   (m)      (Ton)
        //1.     SAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=....

        //2.
        //                                    ------------------------------------
        //                                    Total Steel (M.TON)
        #endregion Inputs

        public Slab_BOQ()
        {
            BeamNos = "";
            Section_B = 0.0;
            Section_D = 0.0;
            Floor_ELevation = 0.0;
            Slab_Thickness = 0.0;
            Floor_Area = 0.0;
            Steel_Reinforcement = new List<Steel_Reinforcement>();
        }

        public string BeamNos;
        public double Section_B;
        public double Section_D;
        public double Floor_ELevation;
        public double Slab_Thickness;
        public double Floor_Area;
        public double Quantity
        {
            get
            {
                return Floor_Area * Slab_Thickness / 1000.0;
            }

        }
        public override string ToString()
        {
            return string.Format("{0,-35} {1,7:f3} X {2,-7:f3} {3,8:f3} {4,9} {5,15:f3} {6,15:f3}",
               BeamNos, Section_B, Section_D, Floor_ELevation, Slab_Thickness, Floor_Area, Quantity);


            //string.Format("{0,-25} {1,8:f3} X {2,-8:f3} {3,10:f3} {4,10} {5,12:f3}   {5,12:f3} X {6:f3} = {7}",
            //   BeamNos, Section_B, Section_D, Floor_ELevation, Slab_Thickness, Floor_Area, (Slab_Thickness / 1000), Quantity)
        }

        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format("Slab Concrete"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Beam Nos                       Section            Floor        Slab        Floor         Quantity"));
            list.Add(string.Format("                               (B X D)          Elevation    Thickness     Area       Area x Thickness"));
            list.Add(string.Format("                                (Sq.m)             (m)         (mm)        (Sq.m)          (Cu.m)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("248 252 247 246               3.85 X 1.67         10.665        180        6.4295        1157.31   "));

            list.Add(string.Format("{0,-25} {1,8:f3} X {2,-8:f3} {3,10:f3} {4,10} {5,12:f3}   {5,12:f3} X {6:f3} = {7}",
               BeamNos, Section_B, Section_D, Floor_ELevation, Slab_Thickness, Floor_Area, (Slab_Thickness / 1000), Quantity));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            int count = 1;

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Slab Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));

            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString());
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,65} = {1,10:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
    public class Beam_BOQ
    {


        #region Inputs
//        list.Add(string.Format(""));
//list.Add(string.Format("Structral Member: BEAM"));
//list.Add(string.Format(""));
//list.Add(string.Format("Beam Concrete"));
//list.Add(string.Format(""));
//list.Add(string.Format("S.No.      Member No.  Section             Floor Elevation          Slab Thickness   Floor Area        Quantity                   "));
//list.Add(string.Format("                                                                  (mm)                      (Sq.m)            Area x Thickness "));
//list.Add(string.Format("                                                                                           (Cu.m)                "));
//list.Add(string.Format("                                                                                "));
//list.Add(string.Format(" 1.        22          300  600         3.8               160             104.0             104.0 x 0.160"));
//list.Add(string.Format(""));
//list.Add(string.Format(" 2."));
//list.Add(string.Format("                                                                      --------------------------------------------"));
//list.Add(string.Format("                                                                      Total Concrete (Cu.m)"));
//list.Add(string.Format(""));
//list.Add(string.Format(""));
//list.Add(string.Format("Beam Steel Reinforcement"));
//list.Add(string.Format(""));
//list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
//list.Add(string.Format("                         (mm)   (m)      (Ton)"));
//list.Add(string.Format("1.     BAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
//list.Add(string.Format(""));
//list.Add(string.Format("2."));
//list.Add(string.Format("                                                                      ------------------------------------"));
//list.Add(string.Format("                                                                      Total Steel (M.TON)"));
//list.Add(string.Format(""));
        
        
        #endregion Inputs

        public Beam_BOQ()
        {

            BeamNos = "";
            Section_B = 0.0;
            Section_D = 0.0;
            Floor_ELevation = 0.0;
            Length = 0.0;
            Area = 0.0;
            Steel_Reinforcement = new List<Steel_Reinforcement>();
        }

        public string BeamNos;
        public double Section_B;
        public double Section_D;
        public double Floor_ELevation;
        public double Length;
        public double Area;
        public double Quantity
        {
            get
            {
                //return Beam_Area * Length / 1000.0;
                return Area * Length;
            }

        }
        public override string ToString()
        {
            //return string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10}",
            //   BeamNos, Section_B, Section_D, Floor_ELevation, Length, Area, Quantity);

            //return string.Format("{0,-35} {1,7:f3} X {2,-7:f3} {3,8:f3}  {4,9:f3} {5,14:f3} {6,15:f3}",
            //  BeamNos, Section_B, Section_D, Floor_ELevation, Length, Area, Quantity);

            return string.Format("{0,-35} {1,10:f3} {2,9:f3} X {3,-6:f3}  {4,9:f3} {5,12:f3} {6,12:f3}",
              BeamNos, Floor_ELevation, Section_B, Section_D, Area, Length, Quantity);

        }

        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();

            //list.Add(string.Format(""));
            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("Beam Nos                       Section            Floor        Slab        Floor         Quantity"));
            //list.Add(string.Format("                               (B X D)          Elevation    Thickness     Area       Area x Thickness"));
            //list.Add(string.Format("                                (Sq.m)             (m)         (mm)        (Sq.m)          (Cu.m)"));
            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            ////list.Add(string.Format("248 252 247 246               3.85 X 1.67         10.665        180        6.4295        1157.31   "));

            //list.Add(string.Format("{0,-25} {1,8:f3} X {2,-8:f3} {3,10:f3} {4,10} {5,12:f3}   {5,12:f3} X {6:f3} = {7}",
            //   BeamNos, Section_B, Section_D, Floor_ELevation, Length, Area, (Length), Quantity));
            //list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));

            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("  Continuos Member Nos.                   Floor          Section        Area          Beam        Quantity"));
            list.Add(string.Format("                                        Elevation                                    Length    Area x Length "));
            list.Add(string.Format("                                           (m)                           (m)         (Sq.m)       (Cu.m)"));
            list.Add(string.Format("----------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format( this.ToString()));
            list.Add(string.Format(""));


            //list.Add(string.Format(""));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area              Unit Weight       Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)             (Ton/m)             (Ton)                                               "));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Beam Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark                                         Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                                                       [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                                                              (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));

            int count = 1;

            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area                    Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [N x A X L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)                    (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));

            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString("BEAM"));
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,90} = {1:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
    public class Column_BOQ
    {


        #region Inputs
        //        list.Add(string.Format(""));
        //list.Add(string.Format("Structral Member: BEAM"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Concrete"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No.      Member No.  Section             Floor Elevation          Slab Thickness   Floor Area        Quantity                   "));
        //list.Add(string.Format("                                                                  (mm)                      (Sq.m)            Area x Thickness "));
        //list.Add(string.Format("                                                                                           (Cu.m)                "));
        //list.Add(string.Format("                                                                                "));
        //list.Add(string.Format(" 1.        22          300  600         3.8               160             104.0             104.0 x 0.160"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(" 2."));
        //list.Add(string.Format("                                                                      --------------------------------------------"));
        //list.Add(string.Format("                                                                      Total Concrete (Cu.m)"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Steel Reinforcement"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
        //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
        //list.Add(string.Format("1.     BAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
        //list.Add(string.Format(""));
        //list.Add(string.Format("2."));
        //list.Add(string.Format("                                                                      ------------------------------------"));
        //list.Add(string.Format("                                                                      Total Steel (M.TON)"));
        //list.Add(string.Format(""));
        #endregion Inputs

        public Column_BOQ()
        {
            ColumnNos = "";
            Section_B = 0.0;
            Section_D = 0.0;
            Floor_ELevation_From = 0.0;
            Floor_ELevation_To = 0.0;
            Length = 0.0;
            Steel_Reinforcement = new List<Steel_Reinforcement>();
        }

        public string ColumnNos;
        public double Section_B;
        public double Section_D;
        
        public double Floor_ELevation_From;
        public double Floor_ELevation_To;

        public double Length;
        public double Area
        {
            get
            {
                return Section_B * Section_D;
            }
        }
        public double Quantity
        {
            get
            {
                return Area * Length;
            }

        }
        public override string ToString()
        {
            //return string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
            //   ColumnNos, Section_B, Section_D, Floor_ELevation_From, Floor_ELevation_To, Length, Area, Quantity);

            return string.Format("{0,-35} {1,7:f3} X {2,-7:f3} {3,8:f3} {4,11:f3}  {5,12:f3} {6,14:f3} {7,15:f3}",
              ColumnNos, Section_B, Section_D, Floor_ELevation_From, Floor_ELevation_To, Length, Area, Quantity);

        }
        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();


            list.Add(string.Format("Column Concrete"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Continuos Member Nos.                   Section       From Floor    To Floor      Column          Area          Quantity"));
            list.Add(string.Format("                                                      Elevation     Elevation     Length                     Area x Length "));
            list.Add(string.Format("                                                         (m)          (m)          (m)           (Sq.m)          (Cu.m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("{0}", this.ToString()));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));


            int count = 1;
            list.Add(string.Format(""));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area              Unit Weight       Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)             (Ton/m)             (Ton)                                               "));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Column Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));

            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString());
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,69} = {1:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
    public class Staircase_BOQ
    {


        #region Inputs
        //        list.Add(string.Format(""));
        //list.Add(string.Format("Structral Member: BEAM"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Concrete"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No.      Member No.  Section             Floor Elevation          Slab Thickness   Floor Area        Quantity                   "));
        //list.Add(string.Format("                                                                  (mm)                      (Sq.m)            Area x Thickness "));
        //list.Add(string.Format("                                                                                           (Cu.m)                "));
        //list.Add(string.Format("                                                                                "));
        //list.Add(string.Format(" 1.        22          300  600         3.8               160             104.0             104.0 x 0.160"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(" 2."));
        //list.Add(string.Format("                                                                      --------------------------------------------"));
        //list.Add(string.Format("                                                                      Total Concrete (Cu.m)"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Steel Reinforcement"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
        //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
        //list.Add(string.Format("1.     BAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
        //list.Add(string.Format(""));
        //list.Add(string.Format("2."));
        //list.Add(string.Format("                                                                      ------------------------------------"));
        //list.Add(string.Format("                                                                      Total Steel (M.TON)"));
        //list.Add(string.Format(""));
        #endregion Inputs

        public Staircase_BOQ()
        {
           



            
        Floor_Level = "";
        FlightNos = 0;
        Slab_Length = 0.0;
        Slab_Width = 0.0;
        Slab_Thickness = 0.0;
        StepNos = 0;
        Step_Height = 0.0;
        Step_Width = 0.0;
        Landing_Slab_Width = 0.0;
        Landing_Slab_Thickness = 0.0;

        Floor_ELevation_From = 0.0;
        Floor_ELevation_To = 0.0;




        }

        public string Floor_Level;
        public int FlightNos;
        public double Slab_Length;
        public double Slab_Width;
        public double Slab_Thickness;
        public int StepNos;
        public double Step_Height;
        public double Step_Width;
        public double Landing_Slab_Width;
        public double Landing_Slab_Thickness;

        public double Floor_ELevation_From;
        public double Floor_ELevation_To;



        public double Quantity_1
        {
            get
            {
                return FlightNos * Slab_Length * Slab_Width * Slab_Thickness;
            }
        }
        public double Quantity_2
        {
            get
            {
                return FlightNos * StepNos * Slab_Width * Step_Width * Slab_Length;
            }

        }


        public double Quantity_3
        {
            get
            {
                return Slab_Width * 2 * Landing_Slab_Width * Landing_Slab_Thickness;
            }

        }


        public double Quantity_Total
        {
            get
            {
                return Quantity_1 + Quantity_2 + Quantity_3 ;
            }

        }


        public override string ToString()
        {
            return "";

            //return string.Format("{0,-35} {1,7:f3} X {2,-7:f3} {3,8:f3} {4,11:f3}  {5,12:f3} {6,14:f3} {7,15:f3}",
            //  FlightNos, Step_Height, Section_D, Floor_ELevation_From, Floor_ELevation_To, Length, Area, Quantity);

            //return string.Format("{0,-35} {1,7:f3} X {2,-7:f3} {3,8:f3} {4,11:f3}  {5,12:f3} {6,14:f3} {7,15:f3}",
            //  FlightNos, Step_Height, Section_D, Floor_ELevation_From, Floor_ELevation_To, Length, Area, Quantity);

        }
        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();


            list.Add(string.Format("Staircase Concrete"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Continuos Member Nos.                   Section       From Floor    To Floor      Column          Area          Quantity"));
            list.Add(string.Format("                                                      Elevation     Elevation     Length                     Area x Length "));
            list.Add(string.Format("                                                         (m)          (m)          (m)           (Sq.m)          (Cu.m)"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));
            list.Add(string.Format("{0}", this.ToString()));


            int count = 1;
            //list.Add(string.Format(""));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area              Unit Weight       Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)             (Ton/m)             (Ton)                                               "));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));

            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Staircase Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));

            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString());
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,90} = {1:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
    public class ISO_Foundation_BOQ
    {
        #region Inputs
        //list.Add(string.Format(""));
        //list.Add(string.Format("Structral Member: BEAM"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Concrete"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No.      Member No.  Section             Floor Elevation          Slab Thickness   Floor Area        Quantity                   "));
        //list.Add(string.Format("                                                                  (mm)                      (Sq.m)            Area x Thickness "));
        //list.Add(string.Format("                                                                                           (Cu.m)                "));
        //list.Add(string.Format("                                                                                "));
        //list.Add(string.Format(" 1.        22          300  600         3.8               160             104.0             104.0 x 0.160"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(" 2."));
        //list.Add(string.Format("                                                                      --------------------------------------------"));
        //list.Add(string.Format("                                                                      Total Concrete (Cu.m)"));
        //list.Add(string.Format(""));
        //list.Add(string.Format(""));
        //list.Add(string.Format("Beam Steel Reinforcement"));
        //list.Add(string.Format(""));
        //list.Add(string.Format("S.No. Bar Mark    Nos.   Dia    Length   Total Weight "));
        //list.Add(string.Format("                         (mm)   (m)      (Ton)"));
        //list.Add(string.Format("1.     BAst1      200     12    3.8      8 x (3.1416 x 0.016 x 0.016 / 4) x 3.8 x 7.9=...."));
        //list.Add(string.Format(""));
        //list.Add(string.Format("2."));
        //list.Add(string.Format("                                                                      ------------------------------------"));
        //list.Add(string.Format("                                                                      Total Steel (M.TON)"));
        //list.Add(string.Format(""));
        #endregion Inputs

        public ISO_Foundation_BOQ()
        {
            ColumnNos = "";
            Footing_Base_L1 = 0.0;
            Footing_Base_B1 = 0.0;
            Footing_Base_H1 = 0.0;

            Pedestal_L2 = 0.0;
            Pedestal_B2 = 0.0;
            Pedestal_H2 = 0.0;

            Footing_Tapper_Height = 0.0;

            Steel_Reinforcement = new List<Steel_Reinforcement>();
        }

        public string ColumnNos;

        public double Footing_Base_L1;
        public double Footing_Base_B1;
        public double Footing_Base_H1;


        public double Pedestal_L2;
        public double Pedestal_B2;
        public double Pedestal_H2;

        public double Footing_Tapper_Height;

        public double Pedestal_Quantity
        {
            get
            {
                return Pedestal_L2 * Pedestal_B2 * Pedestal_H2;
            }
        }
        public double Footing_Base_Quantity
        {
            get
            {
                return Footing_Base_L1 * Footing_Base_B1 * Footing_Base_H1;
            }
        }
        public double Footing_Tapper_Quantity
        {
            get
            {
                return ((((Footing_Base_L1 * Footing_Base_B1) + (Pedestal_L2 * Pedestal_B2)) / 2) * Footing_Tapper_Height);
            }
        }
        public double Total_Foundation_Quantity
        {
            get
            {
                return (Pedestal_Quantity + Footing_Tapper_Quantity + Footing_Base_Quantity);
            }
        }



        public double Footing_Top_Area
        {
            get
            {
                return Pedestal_L2 * Pedestal_B2;
            }
        }

        public double Footing_Bottom_Area
        {
            get
            {
                return Footing_Base_L1 * Footing_Base_B1;
            }
        }


        public override string ToString()
        {

            //1      8           0.540   7.840         1.200          9.408             7.840        0.300         1.257      0.540       0.250       0.135      10.800
            //return "";
            return string.Format("{0,-10} {1,6:f3} {2,7:f3} {3,13:f3} {4,14:f3} {5,17:f3} {6,12:f3} {7,13:f3} {8,10:f3} {9,11:f3} {10,11:f3} {11,11:f3}",
               ColumnNos.Replace("F", ""), 
               Footing_Top_Area, 
               Footing_Bottom_Area,
               Footing_Base_H1,
               Footing_Base_Quantity,
               Footing_Bottom_Area,
               Footing_Tapper_Height,
               Footing_Tapper_Quantity,
               (Pedestal_L2 * Pedestal_B2),
               Pedestal_H2,
               Pedestal_Quantity,
               Total_Foundation_Quantity);
        }

        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();

            list.Add(string.Format(""));
            list.Add(string.Format("Isolated Footing Concrete"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Column        Area of Footing    Thickness       Quantity1         Rect. Area    Thickness     Quantity2  Pedestal    Pedestal    Pedestal    Total"));
            list.Add(string.Format("      Member No.     Top    Bottom       h1 (m.)    [(A1+A2)/2]xh1       of Footing                             Area         Height     Quantity3   Quantity "));
            list.Add(string.Format("                     A1       A2                        (Cu.m)               (Sq.m)       m.          (Cu.m)    (Sq.m.)      (m)         (Cu.m)     (Cu.m)"));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("     " + this.ToString()));
            list.Add(string.Format("-------------------------------------------------------------------------------------------------------------------------------------------------------------"));

            int count = 1;
            //list.Add(string.Format(""));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area              Unit Weight       Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]       [Bw]           [N x L x Bw] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)             (Ton/m)             (Ton)                                               "));
            //list.Add(string.Format("------------------------------------------------------------------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Isolated Footing Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));




            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString());
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,63} = {1:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
    public class Pile_Foundation_BOQ
    {
        #region Inputs
        #endregion Inputs

        public Pile_Foundation_BOQ()
        {
            ColumnNos = "";
            Pile_Dia = 0.0;
            Pile_Length = 0.0;
            Pile_Nos = 0.0;

            Pile_Cap_Length = 0.0;
            Pile_Cap_Width = 0.0;
            Pile_Cap_Thickness = 0.0;

            Steel_Reinforcement = new List<Steel_Reinforcement>();
        }

        public string ColumnNos;
        public double Pile_Dia;
        public double Pile_Area
        {
            get
            {
                return (Math.PI * Pile_Dia * Pile_Dia / 4.0);
            }
        }
        public double Pile_Length;
        public double Pile_Nos;
        public double Quantity_1
        {
            get
            {
                return Pile_Nos * Pile_Area * Pile_Length;
            }
        }
        public double Pile_Cap_Length;
        public double Pile_Cap_Width;

        public double Pile_Cap_Area
        {
            get
            {
                return Pile_Cap_Length * Pile_Cap_Width;
            }
        }
        public double Pile_Cap_Thickness;
        public double Quantity_2
        {
            get
            {
                return Pile_Cap_Area * Pile_Cap_Thickness;
            }
        }


        public double Total_Quantity
        {
            get
            {
                return (Quantity_1 + Quantity_2);
            }
        }




        public override string ToString()
        {
             //1      5           1.000       0.785      25.000           8        157.080         18.920         1.600       30.272      187.352

            //return "";
            return string.Format("{0,-10} {1,6:f3} {2,11:f3} {3,11:f3} {4,11} {5,14:f3} {6,14:f3} {7,13:f3} {8,12:f3} {9,12:f3}",
             ColumnNos.Replace("P", ""),
             Pile_Dia,
             Pile_Area,
             Pile_Length,
             Pile_Nos,
             Quantity_1,
             Pile_Cap_Area,
             Pile_Cap_Thickness,
             Quantity_2,
             Total_Quantity);
            //return string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10}",
            //   ColumnNos, Section_B, Footing_Section_L, Floor_ELevation_From, Floor_ELevation_To, Length, Area, Quantity);
        }

        public List<Steel_Reinforcement> Steel_Reinforcement { get; set; }

        public List<string> Get_Text()
        {
            List<string> list = new List<string>();


            list.Add(string.Format("Pile Foundation Concrete"));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("Column       Pile Dia.   Pile Area   Length       Pile Nos.   Quantity1       Pile Cap    Pile Cap      Quantity2    Total "));
            list.Add(string.Format("Member No.      (m.)      (Sq.m)     of Pile(m.)               (Cu.m)         Area (Sq.m) Thickness(m)   (Cu.m)      Quantity (Cu.m)"));
            list.Add(string.Format("                      [D]         [A]         [h1]        [N]       [N x A x h1]       [AC]        h2       [N x AC x h2]  QTY1 + QTY2 "));
            list.Add(string.Format("------------------------------------------------------------------------------------------------------------------------------------------"));
            list.Add(this.ToString());

            int count = 1;
          
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length             Area            Bar Weight        Total Weight "));
            //list.Add(string.Format("                  [N]     [D]       [L]      [A = 3.1416 x D^2 / 4]     [Bw]        [N x L x 7.9 / 10^6] "));
            //list.Add(string.Format("                         (mm)       (m)             (Sq.mm)            (Ton/m)             (TON)                                               "));
            //list.Add(string.Format("-------------------------------------------------------------------------------------------------"));


            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format("Pile Foundation Steel Reinforcement"));
            list.Add(string.Format("--------------------------------------------------------------------------------"));
            list.Add(string.Format("S.No. Bar Mark    Nos.    Dia     Length          Unit Weight       Total Weight "));
            list.Add(string.Format("                  [N]     [D]       [L]             [Bw]           [N x L x Bw] "));
            list.Add(string.Format("                         (mm)       (m)            (Ton/m)             (Ton)                                               "));
            list.Add(string.Format("--------------------------------------------------------------------------------"));




            double total_steel = 0.0;
            foreach (var item in Steel_Reinforcement)
            {
                item.S_No = count++;
                list.Add(string.Format(""));
                list.Add(item.ToString());
                total_steel += item.Total_Weight;
            }
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format("{0,90} = {1:f4}", "Total Steel (M. Ton) ", total_steel));
            list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            list.Add(string.Format(""));



            //list.Add(string.Format(""));
            //list.Add(string.Format("Beam Nos                       Section (B X D)         Floor Elevation Slab Thickness  Floor Area   QuantityLength "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("248 252 247 246                 3.85       1.67     10.665 180        6.4295     1157.31   "));
            //list.Add(string.Format(""));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("S.No  Bar Mark  Nos.     Bar Dia     Length            Total Length"));
            //list.Add(string.Format("-----------------------------------------------------------------------------------------------------------------"));
            //list.Add(string.Format("1     S_AS[1]    6          20         1.67            6 x (3.1416 x 0.02 x 0.02 / 4) x 1.67 x 7.9 = 0.025"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("2     S_AS[2]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("3     S_AS[3]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("4     S_AS[4]    5          10         1.67            5 x (3.1416 x 0.01 x 0.01 / 4) x 1.67 x 7.9 = 0.005"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("5     S_AS[5]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("6     S_AS[6]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("7     S_AS[7]    6          12         3.85            6 x (3.1416 x 0.012 x 0.012 / 4) x 3.85 x 7.9 = 0.021"));
            //list.Add(string.Format(""));
            //list.Add(string.Format("8     S_AS[8]    5          10         3.85            5 x (3.1416 x 0.01 x 0.01 / 4) x 3.85 x 7.9 = 0.012"));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));
            //list.Add(string.Format(""));


            return list;

        }

    }
}
