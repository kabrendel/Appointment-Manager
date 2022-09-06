using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Appointment_Manager
{
    public class DBObjects
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
        #region User object methods.
        public void LoadUsers()
        {
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            const string sqlString = "SELECT userId, userName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user;";
            MySqlDataReader rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                Users.Add(NewUser(rdr));
            }
            rdr.Close();
            CNObject.ConnectionClose();
        }
        public User NewUser(MySqlDataReader rdr)
        {
            return _ = new User
                (
                    int.Parse(rdr[0].ToString()),
                    rdr[1].ToString(),
                    null,//  password
                    byte.Parse(rdr[2].ToString()),
                    DateTime.SpecifyKind((DateTime)rdr[3], DateTimeKind.Utc),
                    rdr[4].ToString(),
                    DateTime.SpecifyKind((DateTime)rdr[5], DateTimeKind.Utc),
                    rdr[6].ToString()
                );
        }
        #endregion
        #region Customer object methods.
        public void LoadCustomers()
        {
            //  Load customer or refresh collections for Customer screen.
            if (Customers != null)
            {
                Addresses.Clear();
                Cities.Clear();
                Countries.Clear();
                Customers.Clear();
            }
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            //  --------------------------------------------------------
            String sqlString = "SELECT * FROM customer";
            MySqlDataReader rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewCustomer(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM address";
            rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewAddr(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM city";
            rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewCity(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM country";
            rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewCountry(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            CNObject.ConnectionClose();
        }
        public void NewAddr(MySqlDataReader rdr)
        {
            Address temp = new Address
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                rdr[2].ToString(),
                int.Parse(rdr[3].ToString()),
                rdr[4].ToString(),
                rdr[5].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[6], DateTimeKind.Utc),
                rdr[7].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[8], DateTimeKind.Utc),
                rdr[9].ToString()
            );
            Addresses.Add(temp);
        }
        public void NewAddr(int addrId, string ad1, string ad2, int cityId, string postal, string phone, DateTime date, string user, DateTime date1, string user1)
        {
            Address temp = new Address(addrId, ad1, ad2, cityId, postal, phone, date, user, date1, user1);
            Addresses.Add(temp);
        }
        public void NewCity(MySqlDataReader rdr)
        {
            City temp = new City
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                int.Parse(rdr[2].ToString()),
                DateTime.SpecifyKind((DateTime)rdr[3], DateTimeKind.Utc),
                rdr[4].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[5], DateTimeKind.Utc),
                rdr[6].ToString()
            );
            Cities.Add(temp);
        }

        public void NewCity(int cityId, string city, int countryId, DateTime date, string user, DateTime date1, string user1)
        {
            City temp = new City(cityId, city, countryId, date, user, date1, user1);
            Cities.Add(temp);
        }

        public void NewCountry(MySqlDataReader rdr)
        {
            Country temp = new Country
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[2], DateTimeKind.Utc),
                rdr[3].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[4], DateTimeKind.Utc),
                rdr[5].ToString()
            );
            Countries.Add(temp);
        }

        public void NewCountry(int countryId, string country, DateTime date, string user, DateTime date1, string user1)
        {
            Country temp = new Country(countryId, country, date, user, date1, user1);
            Countries.Add(temp);
        }

        public void NewCustomer(MySqlDataReader rdr)
        {
            Customer temp = new Customer
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                int.Parse(rdr[2].ToString()),
                bool.Parse(rdr[3].ToString()),
                DateTime.SpecifyKind((DateTime)rdr[4], DateTimeKind.Utc),
                rdr[5].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[6], DateTimeKind.Utc),
                rdr[7].ToString()
            );
            Customers.Add(temp);
        }

        public void NewCustomer(int customerId, string name, int addrId, bool active, DateTime date, string user, DateTime date1, string user1)
        {
            Customer temp = new Customer(customerId, name, addrId, active, date, user, date1, user1);
            Customers.Add(temp);
        }

        #endregion
        #region Appointment object methods.
        public void LoadAppointments()
        {
            //  Clear current appointments and load all appointments..
            Appointments.Clear();
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            String sqlString = "SELECT * FROM appointment";
            MySqlDataReader rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewAppointment(rdr);
            }
            rdr.Close();
            CNObject.ConnectionClose();
        }
        public void LoadAppointments(int UserId)
        {
            //  Clear current appointments and load logged in users appointments.
            Appointments.Clear();
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            String sqlString = "SELECT * FROM appointment where userId=" + UserId;
            MySqlDataReader rdr = CNObject.ExecuteQuery(sqlString);
            while (rdr.Read())
            {
                NewAppointment(rdr);
            }
            rdr.Close();
            CNObject.ConnectionClose();
        }
        public void NewAppointment(MySqlDataReader rdr)
        {
            Appointment temp = new Appointment
            (
                int.Parse(rdr[0].ToString()),
                int.Parse(rdr[1].ToString()),
                int.Parse(rdr[2].ToString()),
                rdr[3].ToString(),
                rdr[4].ToString(),
                rdr[5].ToString(),
                rdr[6].ToString(),
                rdr[7].ToString(),
                rdr[8].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[9], DateTimeKind.Utc),
                DateTime.SpecifyKind((DateTime)rdr[10], DateTimeKind.Utc),
                DateTime.SpecifyKind((DateTime)rdr[11], DateTimeKind.Utc),
                rdr[12].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[13], DateTimeKind.Utc),
                rdr[14].ToString()
            );
            Appointments.Add(temp);
        }

        public void NewAppointment(int apptId, int custId, int userId, string title, string desc, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime date, string user, DateTime date1, string user1)
        {
            Appointment temp = new Appointment(apptId, custId, userId, title, desc, location, contact, type, url, start, end, date, user, date1, user1);
            Appointments.Add(temp);
        }
        #endregion
    }//  End of Class
}
