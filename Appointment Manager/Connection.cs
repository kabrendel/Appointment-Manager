using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Appointment_Manager
{
    internal class Connection
    {
        private readonly string windowText = "Appointment Manager";
        //  Strings for database.
        private const string server = "localhost";
        private const string database = "clientschedule";
        private const string uid = "Student";
        private const string pass = "student";
        //  Connection string.
        private const string connectionString = "server=" + Appointment_Manager.Connection.server + ";userid=" + Appointment_Manager.Connection.uid + ";" +
            "password=" + Appointment_Manager.Connection.pass + ";database=" + Appointment_Manager.Connection.database + ";";
        //
        public MySqlConnection connection { get; private set; }

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
