using System;
using System.IO;

namespace Appointment_Scheduler
{
    internal static class FileLog
    {
        private const string logname = "userlog.txt";

        static FileLog()
        {
            if (!File.Exists(logname))
            {
                WriteLog("Log file for ClientSchedule.  All times logged in UTC.");
            }
        }

        private static void WriteLog(string message)
        {
            var dt = DateTime.UtcNow;
            using (var sw = File.AppendText(logname))
            {
                sw.WriteLine($"[{dt:s}] {message}");
            }
        }

        public static void Log(bool status, string user)
        {
            WriteLog(status
                            ? $"Success: USER<{user}> has logged in."
                            : $"Failure: USER<{user}> has failed to log in.");
        }
    }
}