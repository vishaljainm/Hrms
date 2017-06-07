using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HyderabadService
{
    class Scheduler
    {
        System.Timers.Timer timer = null;
        double interval = 60000;
        public void Start()
        {    // set the timer which is sccheduled for the desired interval
            timer = new System.Timers.Timer(interval);
            timer.AutoReset = true;
            timer.Enabled = true;
            //start the timer
            timer.Start();
            //event starts when timer has completed a cycle
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ConvertFiles();
        }
        void ConvertFiles()
        {
            try
            {//Calls convert which converts hyderabad files
                ConvertHyderabad.Convert();
                //ConvertDelhi.Convert();
            }
            catch (Exception e)
            {
                string subject = "Regarding Failure While Converting Attendance files of Delhi ";
                string body = "Unable To convert Delhi Files during" + DateTime.Now.ToString() + "Exception is " + e;
                //SendEmail.Send("vishalporwar95@gmail.com", subject, body);
                Logger.Log("Unable to Convert Delhi Files\n Exception is " + e);

            }
            // ConvertHyderabad.Convert();



        }
    }
}

