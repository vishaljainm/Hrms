using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyderabadService
{
    class Logger
    {
        public static void Log(string logMessage)
        {
            var logFilePath = ConfigurationManager.AppSettings["logFilePath"];
            if ()
            {
                try
                {
                    using (StreamWriter w = File.AppendText(logFilePath))
                    {
                        w.Write("\r\nLog Entry : ");
                        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                            DateTime.Now.ToLongDateString());
                        w.WriteLine("  :");
                        w.WriteLine("  :{0}", logMessage);
                        w.WriteLine("-------------------------------");
                    }
                }
                catch(Exception e)
                {
                    // unable to open log file
                }
            }
            
        }
    }
}
