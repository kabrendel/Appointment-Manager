using System;
using System.IO;

namespace Appointment_Manager
{
    class FileLog
    {
        private static readonly string logname = "userlog.txt";

        public void Log(bool status, string user)
        {
            LogExists();
            DateTime time = DateTime.UtcNow;
            if (status)
            {
                string logMsg = "USER <" + user + "> has logged in at <" + time + ">." ;
                using (StreamWriter fs = File.AppendText(logname))
                {
                    fs.WriteLine(logMsg);
                }
            }
            else
            {
                string logMsg = "Failed Login Attempt with USER <" + user + "> at <" + time + ">.";
                using (StreamWriter fs = File.AppendText(logname))
                {
                    fs.WriteLine(logMsg);
                }
            }
        }

        private void LogExists()
        {
            if (!File.Exists(logname))
            {
                using (StreamWriter fs = File.CreateText(logname))
                {
                    fs.WriteLine("Log file for ClientSchedule.  All times logged in UTC.");
                }
            }
        }
    }
}