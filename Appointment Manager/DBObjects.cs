using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public class DBObjects
    {
        //  Collections for database objects.
        public BindingList<Address> Addresses { get; }
        public BindingList<City> Cities { get;}
        public BindingList<Country> Countries { get;}
        public BindingList<Customer> Customers { get;}
        //
        public DBObjects()
        {
            //  Initialize collection lists.
            Addresses = new BindingList<Address>();
            Cities = new BindingList<City>();
            Countries = new BindingList<Country>();
            Customers = new BindingList<Customer>();
        }
        #region User object methods.
        public BindingList<User> GetUsers()
        {
            BindingList<User> users = new BindingList<User>();
            const string sqlString = "SELECT userId, userName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user;";
            using (var connection = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            users.Add(NewUser(rdr));
                        }
                    }
                }
            }
            return users;
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
        public string UserPassword(string user)
        {
            const string sqlString = "SELECT password FROM user WHERE userName = @user";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    try
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                return _ = rdr[0].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving user record from database: " + '\n' + ex, "Appointment Manager");
                    }
                }
            }
            return null;
        }
        public User UserObject(string user)
        {
            const string sqlString = "SELECT userId, userName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user WHERE userName = @user";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    cmd.Parameters.AddWithValue("@user", user);
                    try
                    {
                        using (MySqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                return NewUser(rdr);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving user record from database: " + '\n' + ex, "Appointment Manager");
                    }
                }
            }
            return null;
        }
        #endregion
        #region Customer object methods.
        public BindingList<Customer> GetCustomers()
        {
            BindingList<Customer> list = new BindingList<Customer>();
            using (var connection = Connection.CreateAndOpen())
            {
                const string sqlString = "SELECT * FROM customer";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewCustomerT(rdr));
                        }
                    }
                }
            }
            return list;
        }
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
            using (var connection2 = Connection.CreateAndOpen())
            {
                String sqlString = "SELECT * FROM customer";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            NewCustomer(rdr);
                        }
                    }
                }
                sqlString = "SELECT * FROM address";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            NewAddr(rdr);
                        }
                    }
                }
                sqlString = "SELECT * FROM city";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            NewCity(rdr);
                        }
                    }
                }
                sqlString = "SELECT * FROM country";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            NewCountry(rdr);
                        }
                    }
                }
            }
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

        public Customer NewCustomerT(MySqlDataReader rdr)
        {
            return _ = new Customer
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
        }
        public void NewCustomer(int customerId, string name, int addrId, bool active, DateTime date, string user, DateTime date1, string user1)
        {
            Customer temp = new Customer(customerId, name, addrId, active, date, user, date1, user1);
            Customers.Add(temp);
        }

        #endregion
        #region Appointment object methods.
        public BindingList<Appointment> GetAppointments(int UserId)
        {
            //  Load appointments for logged in user.
            BindingList<Appointment> list = new BindingList<Appointment>();
            string sqlString = "SELECT * FROM appointment where userId=" + UserId;
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewAppointmentT(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public BindingList<Appointment> GetAppointments()
        {
            //  Load all appointments.
            BindingList<Appointment> list = new BindingList<Appointment>();
            const string sqlString = "SELECT * FROM appointment";
            using (var connection = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewAppointmentT(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public Appointment NewAppointmentT(MySqlDataReader rdr)
        {
            return _ = new Appointment
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
        }

        //public void NewAppointment(int apptId, int custId, int userId, string title, string desc, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime date, string user, DateTime date1, string user1)
        //{
        //    Appointment temp = new Appointment(apptId, custId, userId, title, desc, location, contact, type, url, start, end, date, user, date1, user1);
        //    Appointments.Add(temp);
        //}
        #endregion
    }//  End of Class
}
