using System;
using System.Configuration;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Appointment_Scheduler
{
    internal sealed class Connection// : IDisposable
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Database"].ToString();
        //private bool _disposed = false;
        private Connection() { }
        public static MySqlConnection CreateAndOpen()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        //  Disposal
        //public void Dispose()
        //{
        //    if (!_disposed)
        //    {
        //        _connection.Dispose(); // or Close
        //        _disposed = false;
        //    }
        //}
    }
}
