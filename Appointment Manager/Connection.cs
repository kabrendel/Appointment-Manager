using System;
using MySql.Data.MySqlClient;

namespace Appointment_Manager
{
    internal class Connection : IDisposable
    {
        //  Strings for database.
        private const string server = "localhost";
        private const string database = "clientschedule";
        private const string uid = "Student";
        private const string pass = "student";
        //  Connection string.
        private const string connectionString = "server=" + server + ";userid=" + uid + ";" +
            "password=" + pass + ";database=" + database + ";";
        private bool disposed = false;
        //
        private Connection() { }

        public static MySqlConnection CreateAndOpen()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        //  Disposal
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //??
                }
                disposed = true;
            }
        }
        //  End of class
    }
}
