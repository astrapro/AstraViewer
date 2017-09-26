using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MovingLoadAnalysis
{
    public partial class frm_ProgressBar : Form
    {
        static frm_ProgressBar fins = new frm_ProgressBar();
        public static bool On = false;
        public bool Total_Time_On = false;

        public static DateTime dt = DateTime.Now;
        public static frm_ProgressBar Instance
        {
            get
            {
                if (fins == null)
                    fins = new frm_ProgressBar();
                return fins;
            }

        }
        public static int Value
        {
            get
            {
                return fins.progressBar1.Value;
            }
            set
            {
                try
                {
                    //if (value == 0)
                    //{
                    //if (On == true)
                    //    ON("Reading data...");
                    ////}
                    //if (value == 100)
                    //{
                    //    OFF();
                    //}
                    if (fins.progressBar1.InvokeRequired)
                    {

                        fins.progressBar1.BeginInvoke(fins.ctrl_text, fins.progressBar1, value);
                    }
                    else
                        fins.progressBar1.Invoke(fins.ctrl_text, fins.progressBar1, value);

                    if(fins.lbl_percentage.InvokeRequired)
                        fins.lbl_percentage.BeginInvoke(fins.ctrl_text, fins.lbl_percentage, value + "%");
                    else    
                        fins.lbl_percentage.Invoke(fins.ctrl_text, fins.lbl_percentage, value + "%");
                }
                catch (Exception xx) { }
            }
        }
        public static double A, B;
        double last_A = 0.0;
        public static void SetValue(double a, double b)
        {
            if (!On) return;
            A = (a + 1);
            B = b;
            
            Value = (int)(((a + 1) / b) * 100.0);
            //if (fins.InvokeRequired)
            //{
            //    fins.Invoke(fins.ctrl_text, fins, Title + " " + (a + 1).ToString() + " out of " + b.ToString());
            //}
        }

        static string Title = "";
        public static void ON(string title)
        {
            if (On) return;
            OFF();
            On = true;
            thd = null;
            Title = title;
            if (fins == null)
            {
                fins = new frm_ProgressBar();
            }
            thd = new Thread(new ThreadStart(fins.RunThread));
            //thd.SetApartmentState(ApartmentState.STA);
            thd.Start();
        }
        public static void OFF()
        {
            try
            {
                On = false;
                fins.Invoke(fins.cl_frm, fins);
                Instance.Close();
            }
            catch (Exception ex) { }
        }

        public frm_ProgressBar()
        {
            InitializeComponent();
            ctrl_text = new DChangeControlText(ChangeUser_Text);
            cl_frm = new DCloseForm(Close_Form);
            grfx = this.CreateGraphics();
            dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        }

        static Thread thd = null;

        DChangeControlText ctrl_text = null;
        DCloseForm cl_frm = null;
        Graphics grfx = null;


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //this.Refresh();
            //grfx.Flush();
            if (thd == null) return;
            if (thd.ThreadState == ThreadState.Running)
            {
                this.Refresh();
                grfx.DrawLine(Pens.Red, new Point(progressBar1.Value * 3, 0), new Point(progressBar1.Value * 5, 400));
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AbortThread();
            thd = new Thread(new ThreadStart(RunThread));
            thd.Start();

        }

        private void AbortThread()
        {
            try
            {
                On = false;
                thd.Abort();
                //MessageBox.Show("AbortThread()");
            }
            catch (Exception ex) { }
        }

        void Close_Form(Form f)
        {
            try
            {
                f.Close();
            }
            catch (Exception ex) { }
        }
        void ChangeUser_Text(Object ctrl, object val)
        {

            if (ctrl is ProgressBar)
            {
                ProgressBar p = ctrl as ProgressBar;
                p.Value = (int)val;
            }
            else if (ctrl is Label)
            {
                Label l = ctrl as Label;
                l.Text = (string)val;
            }
            else if (ctrl is Form)
            {
                Form f = ctrl as Form;

                //double hours = (DateTime.Now - dt).Hours;
                //double minutes = (DateTime.Now - dt).Minutes;
                //double seconds = (DateTime.Now - dt).Seconds;

                ////string tm = ", tm : ";
                //string tm = "";
                //tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                //      " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                //      " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                //lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

                //double progrs_val = Value;

                //if (progrs_val > 0)
                //{
                //    double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                //    seconds = (int) ((total_estimate_time / progrs_val) * (100.0 - progrs_val));

                //    hours = minutes = 0.0;
                //    if (seconds > 59)
                //    {
                //        minutes =(int)  (seconds / 60);
                //        seconds =(int)  (seconds % 60);
                //    }
                //    if (minutes > 59)
                //    {
                //        hours =(int)  (minutes / 60);
                //        minutes =(int)  (minutes % 60);
                //    }

                //    //tm += ", rem :";
                //    tm = "";
                //    tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                //         " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                //         " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                //    lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);
                //}

                //f.Text = (string)val + tm;
                f.Text = (string)val;
            }
        }
        void RunThread()
        {
            //progressBar1.Invoke(ctrl_text, progressBar1, 0);
            ////progressBar1.Value = 0;
            //for (int i = 0; i <= 100; i++)
            //{
            //    progressBar1.Invoke(ctrl_text, progressBar1, i);
            //    label1.Invoke(ctrl_text, label1, i + "%");
            //    //grfx.DrawLine(Pens.Red, new Point(progressBar1.Value * 4, 0), new Point(progressBar1.Value * 4, 400));
            //    Thread.Sleep(200);
            //}
            try
            {
                if(fins.InvokeRequired)
                    fins.BeginInvoke(cl_frm, fins);
                else
                    fins.Invoke(cl_frm, fins);
            }
            catch (Exception ex) { }
            finally
            {
                fins = new frm_ProgressBar();
                fins.Text = Title;
                fins.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //return;
            double hours = (DateTime.Now - dt).Hours;
            double minutes = (DateTime.Now - dt).Minutes;
            double seconds = (DateTime.Now - dt).Seconds;

            //string tm = ", tm : ";
            string tm = "";
            //tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
            //      " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
            //      " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
            tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");




            tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

            double progrs_val = A;
            if (thd != null)
            {
                if (thd.Priority != ThreadPriority.Highest)
                    thd.Priority = ThreadPriority.Highest;
            }
            if (progrs_val > 0)
            {
                double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                seconds = (int)((total_estimate_time / (A)) * (B - progrs_val));

                hours = minutes = 0.0;
                if (seconds > 59)
                {
                    minutes = (int)(seconds / 60);
                    seconds = (int)(seconds % 60);
                }
                if (minutes > 59)
                {
                    hours = (int)(minutes / 60);
                    minutes = (int)(minutes % 60);
                }
                //tm += ", rem :";
                tm = "";
                tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                     " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                     " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                tm = "" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);

                if (Total_Time_On)
                {
                    hours += DateTime.Now.Hour;
                    minutes += DateTime.Now.Minute;
                    seconds += DateTime.Now.Second;

                    if (seconds > 59)
                    {
                        minutes++;
                        seconds -= 60;
                    }

                    if (minutes > 59)
                    {
                        hours++;
                        minutes -= 60;
                    }

                    tm = "END :" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                    lbl_tr.Invoke(ctrl_text, lbl_tr, tm);
                    //dt
                    tm = "START :" +  dt.Hour.ToString("00") + ":" + dt.Minute.ToString("00") + ":" + dt.Second.ToString("00");
                    //tm = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                    lbl_tm.Invoke(ctrl_text, lbl_tm, tm);
                }

            }
            //if (fins.InvokeRequired)
            //{
            if (last_A == A)
            {
                this.Close();
            }
            fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString() + " (" + (A-last_A) + "/s)");
            last_A = A;
            //}
        }
        private void timer1_Tick1(object sender, EventArgs e)
        {
            //return;
            double hours = (DateTime.Now - dt).Hours;
            double minutes = (DateTime.Now - dt).Minutes;
            double seconds = (DateTime.Now - dt).Seconds;

            //string tm = ", tm : ";
            string tm = "";
            tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                  " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                  " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
            lbl_total_time.Invoke(ctrl_text, lbl_total_time, tm);

            double progrs_val = A;
            if (thd != null)
                thd.Priority = ThreadPriority.Highest;
            if (progrs_val > 0)
            {
                double total_estimate_time = (hours * 60 * 60 + minutes * 60 + seconds);
                seconds = (int)((total_estimate_time / (A)) * (B - progrs_val));

                hours = minutes = 0.0;
                if (seconds > 59)
                {
                    minutes = (int)(seconds / 60);
                    seconds = (int)(seconds % 60);
                }
                if (minutes > 59)
                {
                    hours = (int)(minutes / 60);
                    minutes = (int)(minutes % 60);
                }
                //tm += ", rem :";
                tm = "";
                tm += " " + ((hours == 0.0) ? "" : hours.ToString() + " h,") +
                     " " + ((minutes == 0.0) ? "" : minutes.ToString() + " m,") +
                     " " + ((seconds == 0.0) ? "" : seconds.ToString() + " s");
                lbl_time_remain.Invoke(ctrl_text, lbl_time_remain, tm);

                if (Total_Time_On)
                {
                    hours += DateTime.Now.Hour;
                    minutes += DateTime.Now.Minute;
                    seconds += DateTime.Now.Second;

                    if (seconds > 59)
                    {
                        minutes++;
                        seconds -= 60;
                    }

                    if (minutes > 59)
                    {
                        hours++;
                        minutes -= 60;
                    }

                    tm = "EST.TM :" + hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
                    lbl_tr.Invoke(ctrl_text, lbl_tr, tm);

                    tm = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                    lbl_tm.Invoke(ctrl_text, lbl_tm, tm);
                }

            }
            //if (fins.InvokeRequired)
            //{
            fins.Invoke(fins.ctrl_text, fins, Title + " " + A.ToString() + " out of " + B.ToString());
            //}
        }

        private void lbl_percentage_Click(object sender, EventArgs e)
        {
            Total_Time_On = !Total_Time_On;
            lbl_tm.Invoke(ctrl_text, lbl_tm, "Total Time :");
            lbl_tr.Invoke(ctrl_text, lbl_tr, "Time Remaining :");
        }
    }
    public delegate void DChangeControlText(Object ctrl, object val);
    public delegate void DCloseForm(Form f);
}
