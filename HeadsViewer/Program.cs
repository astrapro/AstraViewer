using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace HeadsViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new MainForm());
        //}
        [STAThread]
        static void Main(string[] args)
        {
            #region Exceptiopn Handling
            Application.ThreadException += new ThreadExceptionEventHandler(Form1_UIThreadException);
            
            // Set the unhandled exception mode to force all Windows Forms errors to go through 
            // our handler.
            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException, true);
            #endregion Exceptiopn Handling



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm frm = new MainForm();
            if (args.Length > 0)
            {
                frm.startFileName = args[0];
                //frm.startFileName = args;
                Application.Run(frm);
            }
            else
            {
                try
                {
                    //string ff = @"C:\Users\user\Desktop\ASTRA\ASTRA Pro Examples\[01] Static Analysis of Frame [Beam]\DRawing To Data\INPUT8.dwg";
                    //Environment.SetEnvironmentVariable("OPENFILE", ff);
                    //Chiranjit [2014 11 30]
                    string ss = Environment.GetEnvironmentVariable("OPENFILE");
                    if (ss != null && ss != "")
                    {
                        frm.startFileName = ss;
                        Environment.SetEnvironmentVariable("OPENFILE", "");
                    }
                    //Application.Run(new MainForm());
                    Application.Run(frm);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.ToString());
                    //Application.Run(new MainForm());
                }
            }
        }

        
        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            //DialogResult result = DialogResult.Cancel;
            //try
            //{  
            //}
            //catch
            //{
               
            //}
        }


    }
}