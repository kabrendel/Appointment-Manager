using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public class DBObjects
    {
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
        public BindingList<User> GetUserList()
        {
            BindingList<User> list = new BindingList<User>();
            const string sqlString = "SELECT userName,userId FROM user";
            using (var connection = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var user = new User
                            {
                                UserName = rdr[0].ToString(),
                                UserId = int.Parse(rdr[1].ToString())
                            };
                            list.Add(user);
                        }
                    }
                }
            }
            return list;
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
        #endregion
        #region Customer object methods.
        public BindingList<Address> GetAddresses()
        {
            BindingList<Address> list = new BindingList<Address>();
            using (var connection = Connection.CreateAndOpen())
            {
                const string sqlString = "SELECT * FROM address";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewAddr(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public BindingList<City> GetCities()
        {
            BindingList<City> list = new BindingList<City>();
            using (var connection = Connection.CreateAndOpen())
            {
                const string sqlString = "SELECT * FROM city";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewCity(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public BindingList<Country> GetCountries()
        {
            BindingList<Country> list = new BindingList<Country>();
            using (var connection = Connection.CreateAndOpen())
            {
                const string sqlString = "SELECT * FROM country";
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(NewCountry(rdr));
                        }
                    }
                }
            }
            return list;
        }
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
                            list.Add(NewCustomer(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public BindingList<Customer> GetCustomerList()
        {
            BindingList<Customer> list = new BindingList<Customer>();
            const string sqlString = "SELECT customerName,customerId FROM customer";
            using (var connection = Connection.CreateAndOpen())
            using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    var customer = new Customer
                    {
                        CustomerName = rdr[0].ToString(),
                        CustomerId = int.Parse(rdr[1].ToString())
                    };
                    list.Add(customer);
                }
            }
            return list;
        }
        public Address NewAddr(MySqlDataReader rdr)
        {
            return _ = new Address
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
        }
        public City NewCity(MySqlDataReader rdr)
        {
            return _ = new City
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                int.Parse(rdr[2].ToString()),
                DateTime.SpecifyKind((DateTime)rdr[3], DateTimeKind.Utc),
                rdr[4].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[5], DateTimeKind.Utc),
                rdr[6].ToString()
            );
        }
        public Country NewCountry(MySqlDataReader rdr)
        {
            return _ = new Country
            (
                int.Parse(rdr[0].ToString()),
                rdr[1].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[2], DateTimeKind.Utc),
                rdr[3].ToString(),
                DateTime.SpecifyKind((DateTime)rdr[4], DateTimeKind.Utc),
                rdr[5].ToString()
            );
        }
        public Customer NewCustomer(MySqlDataReader rdr)
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
                            list.Add(NewAppointment(rdr));
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
                            list.Add(NewAppointment(rdr));
                        }
                    }
                }
            }
            return list;
        }
        public Appointment NewAppointment(MySqlDataReader rdr)
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
        #endregion
    }//  End of Class
}
