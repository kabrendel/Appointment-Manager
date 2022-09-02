using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Appointment_Manager
{
    internal class Connection
    {
        private string windowText = "Appointment Manager";
        //  Strings for database.
        private static readonly string server = "localhost";
        private static readonly string database = "clientschedule";
        private static readonly string uid = "root";
        private static readonly string pass = "student";
        //  Connection string.
        private static readonly string connectionString = "server=" + Connection.server + ";" + "userid=" + Connection.uid + ";" +
            "password=" + Connection.pass + ";" + "database=" + Connection.database + ";";
        //
        private MySqlConnection connection { get; set; }
        //
        public void CreateConnection()
        {
            //  Try connecting to database.  Return a connection or show error message.
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not create connection to database: " + '\n' + ex, windowText);
            }
        }
        public void ConnectionOpen()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection to database: " + '\n' + ex, windowText);
            }
        }
        public void ConnectionClose()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not close connection to database: " + '\n' + ex, windowText);
            }
        }
        public MySqlDataReader ExecuteQuery(string sqlString)
        {
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public MySqlDataAdapter SQLAdapter(string sqlString)
        {
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            return adapter;
        }
    }// End of class
}
