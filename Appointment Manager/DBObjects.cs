using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager
{
    internal class DBObjects
    {
        //  Collections for database objects.
        public BindingList<Address> Addresses { get; private set; }
        public BindingList<Appointment> Appointments { get; private set; }
        public BindingList<City> Cities { get; private set; }
        public BindingList<Country> Countries { get; private set; }
        public BindingList<Customer> Customers { get; private set; }
        public BindingList<User> Users { get; private set; }
        //

        public DBObjects()
        {
            //  Initialize collection lists.
            Addresses = new BindingList<Address>();
            Appointments = new BindingList<Appointment>();
            Cities = new BindingList<City>();
            Countries = new BindingList<Country>();
            Customers = new BindingList<Customer>();
            Users = new BindingList<User>();
        }

        public void LoadUsers()
        {
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            string sqlString = "SELECT userId, userName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user;";
            MySqlDataReader rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                Users.Add(NewUser(false, rdr));
            }
            rdr.Close();
            CNObject.ConnectionClose();
        }
    }//  End of Class
}
