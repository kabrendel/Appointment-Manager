using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Appointment_Manager
{
    public partial class Main : Form
    {
        //  Objects for Windows.
        private Login Login;
        private Customers Customer;
        private Appointments Aptmnts;
        private Search Search;
        //  Strings for database.
        //  local database.
        private static readonly string server = "localhost";
        private static readonly string database = "clientschedule";
        private static readonly string uid = "root";
        private static readonly string pass = "student";
        //  task database.
        //private static readonly string server = "localhost";
        //private static readonly string database = "client_schedule";
        //private static readonly string uid = "sqlUser";
        //private static readonly string pass = "Passw0rd!";
        //  Connection string.
        private static readonly string connectionString = "server=" + Main.server + ";" + "userid=" + Main.uid + ";" +
            "password=" + Main.pass + ";" + "database=" + Main.database + ";";
        //  Index of next table rows.
        private int customerIndex;
        private int addressIndex;
        private int cityIndex;
        private int countryIndex;
        private int appointmentIndex;
        //  Collections for database objects.
        public BindingList<Address> Addresses;
        public BindingList<Appointment> Appointments;
        public BindingList<City> Cities;
        public BindingList<Country> Countries;
        public BindingList<Customer> Customers;
        public BindingList<User> Users;
        //  Logged in User object.
        public User User;

        public Main()
        {
            InitializeComponent();
            //  Initialize collection lists.
            Addresses = new BindingList<Address>();
            Appointments = new BindingList<Appointment>();
            Cities = new BindingList<City>();
            Countries = new BindingList<Country>();
            Customers = new BindingList<Customer>();
            Users = new BindingList<User>();
            //  
            //  Load appointments for initial view.
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            String sqlString = "SELECT * FROM appointment";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NewAppointment(rdr);
            }
            rdr.Close();
            //  Fill collection of users for creating appointments.
            sqlString = "SELECT * FROM user";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Users.Add(NewUser(rdr));
            }
            rdr.Close();
            connection.Close();
            //  Fill collections for customers
            LoadCustomers();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Login = new Login(this);
            var confirm = Login.ShowDialog();
            if (confirm == DialogResult.OK)
            {
                UserNotification();
                //  Set datasource and view for Main's dataGridView1;
                UpdateAppointments();
            }

        }

        private MySqlConnection ConnectToDB()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                //  Make sure we can connect to database.
                connection.Open();
                connection.Close();
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection to database: " + '\n' + ex, this.Text);
                return null;
            }
        }

        public void UpdateAppointments()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = BuildAppointmentTable();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[User Id] = {0}", User.UserId);
        }

        private void UpdateIndex()
        {
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            //  --------------------------------------------------------
            string sqlString = "SELECT MAX(customerId) from customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                customerIndex = int.Parse(rdr[0].ToString()) + 1;
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT MAX(addressId) from address";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                addressIndex = int.Parse(rdr[0].ToString()) + 1;
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT MAX(cityId) from city";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cityIndex = int.Parse(rdr[0].ToString()) + 1;
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT MAX(countryId) from country";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                countryIndex = int.Parse(rdr[0].ToString()) + 1;
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT MAX(appointmentId) from appointment";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                appointmentIndex = int.Parse(rdr[0].ToString()) + 1;
            }
            rdr.Close();
            //  --------------------------------------------------------
            connection.Close();
        }

        private void NewAddr(MySqlDataReader rdr)
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

        private void NewAddr(int addrId, string ad1, string ad2, int cityId, string postal, string phone, DateTime date, string user, DateTime date1, string user1)
        {
            Address temp = new Address(addrId,ad1,ad2,cityId,postal,phone,date,user,date1,user1);
            Addresses.Add(temp);
        }

        private void NewAppointment(MySqlDataReader rdr)
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

        private void NewAppointment(int apptId, int custId, int userId, string title, string desc, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime date, string user, DateTime date1, string user1)
        {
            Appointment temp = new Appointment(apptId,custId,userId,title,desc,location,contact,type,url,start,end,date,user,date1,user1);
            Appointments.Add(temp);
        }

        private void NewCity(MySqlDataReader rdr)
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

        private void NewCity(int cityId, string city, int countryId, DateTime date, string user, DateTime date1, string user1)
        {
            City temp = new City(cityId,city,countryId,date,user,date1,user1);
            Cities.Add(temp);
        }

        private void NewCountry(MySqlDataReader rdr)
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
        
        private void NewCountry(int countryId, string country, DateTime date, string user, DateTime date1,string user1)
        {
            Country temp = new Country(countryId,country,date,user,date1,user1);
            Countries.Add(temp);
        }

        private void NewCustomer(MySqlDataReader rdr)
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
        
        private void NewCustomer(int customerId, string name, int addrId, bool active, DateTime date, string user, DateTime date1, string user1)
        {
            Customer temp = new Customer(customerId,name,addrId,active,date,user,date1,user1);
            Customers.Add(temp);
        }

        private User NewUser(MySqlDataReader rdr)
        {
            return User = new User
                    (
                       int.Parse(rdr[0].ToString()),
                       rdr[1].ToString(),
                       rdr[2].ToString(),
                       byte.Parse(rdr[3].ToString()),
                       DateTime.SpecifyKind((DateTime)rdr[4], DateTimeKind.Utc),
                       rdr[5].ToString(),
                       DateTime.SpecifyKind((DateTime)rdr[6], DateTimeKind.Utc),
                       rdr[7].ToString()
                    );
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
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            //  --------------------------------------------------------
            String sqlString = "SELECT * FROM customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NewCustomer(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM address";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NewAddr(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM city";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NewCity(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            sqlString = "SELECT * FROM country";
            cmd = new MySqlCommand(sqlString, connection);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                NewCountry(rdr);
            }
            rdr.Close();
            //  --------------------------------------------------------
            connection.Close();
            UpdateIndex();
        }

        public DataTable UserList(bool all)
        {
            //  Connect to database.
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            String sqlString = "SELECT userName,userId FROM user";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            //  ---------------------------------
            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            while (rdr.Read())
            {
                DataRow row = dataTable.NewRow();
                row["Name"] = rdr[0].ToString();
                row["ID"] = int.Parse(rdr[1].ToString());
                dataTable.Rows.Add(row);
            }
            rdr.Close();
            connection.Close();
            if (!all)
            {
                //  Filter to just the logged in users appointments.
                dataTable.DefaultView.RowFilter = String.Format("[Id] = {0}",User.UserId);
            }
            return dataTable;
        }

        public DataTable CustomerList()
        {
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            String sqlString = "SELECT customerName,customerId FROM customer";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            while (rdr.Read())
            {
                DataRow row = dataTable.NewRow();
                row["Name"] = rdr[0].ToString();
                row["ID"] = int.Parse(rdr[1].ToString());
                dataTable.Rows.Add(row);
            }
            rdr.Close();
            connection.Close();
            return dataTable;
        }

        public List<string> TypeList()
        {
            //  Connect to database.
            //MySqlConnection connection = ConnectToDB();
            //connection.Open();
            //String sqlString = "SELECT DISTINCT type FROM appointment";
            //MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            ////  ---------------------------------
            //MySqlDataReader rdr = cmd.ExecuteReader();
            //List<string> list = new List<string>();
            //while (rdr.Read())
            //{
            //    list.Add(rdr[0].ToString());
            //}
            //rdr.Close();
            //connection.Close();
            //  Changed to match evaluation issue..
            List<string> list = new List<string>();
            list.Add("Test");
            list.Add("Scrum");
            list.Add("Presentation");
            return list;
        }

        public bool CreateAppointment(int customerId, int userId, string type, DateTime start, DateTime end)
        {
            string title = "not needed";
            string desc = "not needed";
            string location = "not needed";
            string contact = "not needed";
            string url = "not needed";
            string sqlCommand = "INSERT INTO appointment VALUES (@appointmentId,@customer,@user,@title,@desc,@loc,@cont,@type,@url,@start,@end,@time1,@user1,@time2,@user2)";

            UpdateIndex();

            MySqlConnection connection = ConnectToDB();
            connection.Open();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, connection);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentIndex);
                cmd.Parameters.AddWithValue("@customer", customerId);
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@loc", location);
                cmd.Parameters.AddWithValue("@cont", contact);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.Parameters.AddWithValue("@start", start.ToUniversalTime());
                cmd.Parameters.AddWithValue("@end", end.ToUniversalTime());
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user2", User.UserName);
                cmd.ExecuteNonQuery();
                NewAppointment(appointmentIndex, customerId, userId, title, desc, location, contact, type, url, start, end, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool UpdateAppointment(int appointmentId,int customerId, int userId, string type, DateTime start, DateTime end)
        {
            string title = "not needed";
            string desc = "not needed";
            string location = "not needed";
            string contact = "not needed";
            string url = "not needed";
            string sqlCommand = "UPDATE appointment SET customerId = @customer, userId = @user,type = @type,start = @start,end = @end,lastUpdate = @time2,lastUpdateBy = @user2 WHERE appointmentId = @appointmentId";

            MySqlConnection connection = ConnectToDB();
            connection.Open();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, connection);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                cmd.Parameters.AddWithValue("@customer", customerId);
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@start", start.ToUniversalTime());
                cmd.Parameters.AddWithValue("@end", end.ToUniversalTime());
                cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user2", User.UserName);
                cmd.ExecuteNonQuery();
                //  Lambda expression to find our object and remove the old one from our collection.
                //  One line of code instead of 11 lines for a method to do the same thing.
                foreach (Appointment apt in Appointments.Where(x => x.AppointmentId == appointmentId).ToList()) Appointments.Remove(apt);
                NewAppointment(appointmentId, customerId, userId, title, desc, location, contact, type, url, start, end, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool RemoveAppointment(int appointmentId)
        {
            string sqlCommand = "DELETE FROM appointment WHERE appointmentId=@appointmentId";
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, connection);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                cmd.ExecuteNonQuery();
                //  Lambda expression to find our object and remove the old one from our collection.
                //  One line of code instead of 11 lines for a method to do the same thing.
                foreach (Appointment apt in Appointments.Where(x => x.AppointmentId == appointmentId).ToList()) Appointments.Remove(apt);
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing records from database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public DataTable BuildAppointmentTable()
        {
            //  Build a DataTable to show appointments in Appointments form.
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("User Id", typeof(int));              //  User table.
            dataTable.Columns.Add("User Name", typeof(string));         //  User table.
            dataTable.Columns.Add("Customer Id", typeof(int));          //  Customer table.
            dataTable.Columns.Add("Customer Name", typeof(string));     //  Customer table.
            dataTable.Columns.Add("Appointment Id", typeof(int));       //  Appointment table.
            dataTable.Columns.Add("Type", typeof(string));              //  Appointment table.
            dataTable.Columns.Add("Title", typeof(string));             //  Appointment table.
            dataTable.Columns.Add("Start", typeof(DateTime));           //  Appointment table.
            dataTable.Columns["Start"].DateTimeMode = DataSetDateTime.Local;
            dataTable.Columns.Add("End", typeof(DateTime));             //  Appointment table.
            dataTable.Columns["End"].DateTimeMode = DataSetDateTime.Local;
            foreach (Appointment a in Appointments)
            {
                DataRow row = dataTable.NewRow();
                row["Appointment Id"] = a.AppointmentId;
                row["Title"] = a.Title;
                row["Start"] = a.Start;
                row["End"] = a.End;
                row["Type"] = a.Type;
                foreach (Customer c in Customers)
                {
                    if (a.CustomerId == c.CustomerId)
                    {
                        row["Customer Id"] = c.CustomerId;
                        row["Customer Name"] = c.CustomerName;
                        break;
                    }
                }
                foreach (User u in Users)
                {
                    if (a.UserId == u.UserId)
                    {
                        row["User Id"] = u.UserId;
                        row["User Name"] = u.UserName;
                        break;
                    }
                }
                dataTable.Rows.Add(row);
                
            }
            dataTable.DefaultView.Sort = "Start ASC";
            return dataTable;
        }

        public DataTable BuildCustomerTable()
        {
            //  Build a DataTable to show customer data in Customer Form.
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Customer Id", typeof(int));          //  customer table.
            dataTable.Columns.Add("Customer Name", typeof(string));     //  customer table.
            dataTable.Columns.Add("Address Id", typeof(string));        //  address table.
            dataTable.Columns.Add("Address1", typeof(string));          //  address table.
            dataTable.Columns.Add("Address2", typeof(string));          //  address table.
            dataTable.Columns.Add("Postal Code", typeof(string));       //  address table.
            dataTable.Columns.Add("City Id", typeof(string));           //  city table.
            dataTable.Columns.Add("City", typeof(string));              //  city table.
            dataTable.Columns.Add("Country Id", typeof(string));        //  country table.
            dataTable.Columns.Add("Country", typeof(string));           //  country table.
            dataTable.Columns.Add("Phone Number", typeof(string));      //  address table.
            foreach (Customer c in Customers)
            {
                DataRow row = dataTable.NewRow();
                row["Customer Id"] = c.CustomerId;
                row["Customer Name"] = c.CustomerName;
                foreach (Address a in Addresses)
                {
                    if (a.AddressId == c.AddressId)
                    {
                        row["Address Id"] = a.AddressId;
                        row["Address1"] = a.Address1;
                        row["Address2"] = a.Address2;
                        row["Postal Code"] = a.PostalCode;
                        row["Phone Number"] = a.Phone;
                        foreach (City i in Cities)
                        {
                            if (i.CityId == a.CityId)
                            {
                                row["City Id"] = i.CityId;
                                row["City"] = i.city;
                                foreach (Country y in Countries)
                                {
                                    if (y.CountryId == i.CountryId)
                                    {
                                        row["Country Id"] = y.CountryId;
                                        row["Country"] = y.country;
                                        goto Rowbuilt;
                                    }
                                }

                            }
                        }
                    }
                }
            Rowbuilt:
                dataTable.Rows.Add(row);
            }
            DataRow blank = dataTable.NewRow();
            blank["Customer Id"] = customerIndex;
            blank["Customer Name"] = "New Customer";
            blank["Address Id"] = "";
            blank["Address1"] = "";
            blank["Address2"] = "";
            blank["Postal Code"] = "";
            blank["Phone Number"] = "";
            blank["City Id"] = "";
            blank["City"] = "";
            blank["Country Id"] = "";
            blank["Country"] = "";
            dataTable.Rows.Add(blank);
            dataTable.DefaultView.Sort = "Customer Id ASC";
            return dataTable;
        }

        public bool AddCustomer(int customerId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            if (customerId != customerIndex)
            {
                //  customerId should match customerIndex else it's not a new customer.
                return false;
            }
            else
            {
                string countryString = "INSERT INTO country VALUES (@countryId,@country,@time1,@user1,@time2,@user2)";
                string cityString = "INSERT INTO city VALUES (@cityId,@city,@countryId,@time1,@user1,@time2,@user2)";
                string addrString = "INSERT INTO address values (@addrId,@ad1,@ad2,@cityId,@postal,@phone,@time1,@user1,@time2,@user2)";
                string custString = "INSERT INTO customer VALUES (@customerId,@name,@addrId,@bool,@time1,@user1,@time2,@user2)";
                
                UpdateIndex();

                MySqlConnection connection = ConnectToDB();
                connection.Open();
                MySqlCommand cmd;
                try
                {
                    cmd = new MySqlCommand(countryString, connection);
                    cmd.Parameters.AddWithValue("@countryId", countryIndex);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    cmd.ExecuteNonQuery();
                    NewCountry(countryIndex,country,DateTime.UtcNow,User.UserName,DateTime.UtcNow,User.UserName);

                    cmd = new MySqlCommand(cityString, connection);
                    cmd.Parameters.AddWithValue("@cityId", cityIndex);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@countryId", countryIndex);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    cmd.ExecuteNonQuery();
                    NewCity(cityIndex, city, countryIndex, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                    cmd = new MySqlCommand(addrString, connection);
                    cmd.Parameters.AddWithValue("@addrId", addressIndex);
                    cmd.Parameters.AddWithValue("@ad1", ad1);
                    cmd.Parameters.AddWithValue("@ad2", ad2);
                    cmd.Parameters.AddWithValue("@cityId", cityIndex);
                    cmd.Parameters.AddWithValue("@postal", postal);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    cmd.ExecuteNonQuery();
                    NewAddr(addressIndex,ad1,ad2,cityIndex,postal,phone,DateTime.UtcNow,User.UserName,DateTime.UtcNow,User.UserName);

                    cmd = new MySqlCommand(custString, connection);
                    cmd.Parameters.AddWithValue("@customerId", customerIndex);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@addrId", addressIndex);
                    cmd.Parameters.AddWithValue("@bool", true);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    cmd.ExecuteNonQuery();
                    NewCustomer(customerIndex,name,addressIndex,true,DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                    return false;
                }
            }
        }

        public bool UpdateCustomer(int customerId, int addressId, int cityId, int countryId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            string countryString = "UPDATE country SET country = @country, lastUpdate = @time1, lastUpdateBy = @user1 WHERE countryid = @countryId";
            string cityString = "UPDATE city SET city = @city, countryId = @countryId, lastUpdate = @time1, lastUpdateBy = @user1 WHERE cityid = @cityId";
            string addrString = "UPDATE address SET address = @ad1, address2 = @ad2, cityId = @cityId, postalCode = @postal, phone = @phone, lastUpdate = @time1, lastUpdateBy = @user1 WHERE addressId = @addrId";
            string custString = "UPDATE customer SET customerName = @name, addressId = @addrId, active = @bool, lastUpdate = @time1, lastUpdateBy = @user1 WHERE customerId = @customerId";

            MySqlConnection connection = ConnectToDB();
            connection.Open();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(countryString, connection);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.ExecuteNonQuery();
                //  Lambda expression to find our object and remove the old one from our collection.
                foreach (Country c in Countries.Where(x => x.CountryId == countryId).ToList()) Countries.Remove(c);
                NewCountry(countryId, country, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(cityString, connection);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.ExecuteNonQuery();
                foreach (City c in Cities.Where(x => x.CityId == cityId).ToList()) Cities.Remove(c);
                NewCity(cityId, city, countryId, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(addrString, connection);
                cmd.Parameters.AddWithValue("@ad1", ad1);
                cmd.Parameters.AddWithValue("@ad2", ad2);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.Parameters.AddWithValue("@postal", postal);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@addrId", addressId);
                cmd.ExecuteNonQuery();
                foreach (Address a in Addresses.Where(x => x.AddressId == addressId).ToList()) Addresses.Remove(a);
                NewAddr(addressId, ad1, ad2, cityId, postal, phone, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(custString, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@addrId", addressId);
                cmd.Parameters.AddWithValue("@bool", true);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.ExecuteNonQuery();
                foreach (Customer c in Customers.Where(x => x.CustomerId == customerId).ToList()) Customers.Remove(c);
                NewCustomer(customerId, name, addressId, true, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating records in database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool DeleteCustomer(int cust, int addr, int city, int cntry)
        {
            string countryString = "DELETE FROM country WHERE countryId=@countryId";
            string cityString = "DELETE FROM city WHERE cityId=@cityId";
            string addrString = "DELETE FROM address WHERE addressId=@addressId";
            string custString = "DELETE FROM customer WHERE customerId=@customerId";

            MySqlConnection connection = ConnectToDB();
            connection.Open();
            MySqlCommand cmd;
            object reader;
            try
            {
                // check for appointments, unable to delete a customer with appointments.
                string custcheck = "select customerId from appointment where customerId=@cust";
                cmd = new MySqlCommand(custcheck, connection);
                cmd.Parameters.AddWithValue("@cust", cust);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    //  No rows returned from appointment table, safe to delete customer.
                    cmd = new MySqlCommand(custString, connection);
                    cmd.Parameters.AddWithValue("@customerId", cust);
                    cmd.ExecuteNonQuery();
                    foreach (Customer c in Customers.Where(x => x.CustomerId == cust).ToList()) Customers.Remove(c);
                }
                else
                {
                    // prompt to delete appointments?
                    var confirmDelete = MessageBox.Show("Customer has scheduled appointments, delete appointments?", this.Text, MessageBoxButtons.OKCancel);
                    if (confirmDelete == DialogResult.OK)
                    {
                        //  remove appointments from table
                        string delapts = "delete from appointment where customerId=@cust";
                        cmd = new MySqlCommand(delapts, connection);
                        cmd.Parameters.AddWithValue("@cust", cust);
                        cmd.ExecuteNonQuery();
                        foreach (Appointment apt in Appointments.Where(x => x.CustomerId == cust).ToList()) Appointments.Remove(apt);
                        //  remove customer
                        cmd = new MySqlCommand(custString, connection);
                        cmd.Parameters.AddWithValue("@customerId", cust);
                        cmd.ExecuteNonQuery();
                        foreach (Customer c in Customers.Where(x => x.CustomerId == cust).ToList()) Customers.Remove(c);
                    }
                    else
                    {
                        return false;
                    }
                }
                // remove address if no associated customer reference
                string addrcheck = "select addressId from customer where addressId=@addr";
                cmd = new MySqlCommand(addrcheck, connection);
                cmd.Parameters.AddWithValue("@addr", addr);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    //  will be null if no rows in table match.
                    cmd = new MySqlCommand(addrString, connection);
                    cmd.Parameters.AddWithValue("@addressId", addr);
                    cmd.ExecuteNonQuery();
                    foreach (Address a in Addresses.Where(x => x.AddressId == addr).ToList()) Addresses.Remove(a);
                }
                // remove city if no associated address reference
                string citycheck = "select cityId from address where cityId=@city";
                cmd = new MySqlCommand(citycheck, connection);
                cmd.Parameters.AddWithValue("@city", city);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    cmd = new MySqlCommand(cityString, connection);
                    cmd.Parameters.AddWithValue("@cityId", city);
                    cmd.ExecuteNonQuery();
                    foreach (City c in Cities.Where(x => x.CityId == city).ToList()) Cities.Remove(c);
                }
                // remove country if no associated city reference
                string cntrycheck = "select countryId from city where countryId=@cntry";
                cmd = new MySqlCommand(cntrycheck, connection);
                cmd.Parameters.AddWithValue("@cntry", cntry);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    cmd = new MySqlCommand(countryString, connection);
                    cmd.Parameters.AddWithValue("@countryId", cntry);
                    cmd.ExecuteNonQuery();
                    foreach (Country c in Countries.Where(x => x.CountryId == cntry).ToList()) Countries.Remove(c);
                }
                //
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Error removing records from database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public string[] UserLogin(string user, string pass)
        {
            if (User != null)
            {
                // Reset user object created below.
                User = null;
            }
            string[] results = new string[2];
            //  database connection.
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            String sqlString = "SELECT * FROM user WHERE userName = @user";
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            cmd.Parameters.AddWithValue("@user", user);
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User = NewUser(rdr);
                }
                rdr.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("Error retrieving user record from database: " + '\n'+ ex,this.Text);
                results[0] = "False";
                results[1] = "DB";
                return results;
            }
            if (User == null)
            {
                results[0] = "False";
                results[1] = "User";
                return results;
            }
            else
            {
                if (User.Password == pass)
                {
                    results[0] = "True";
                    results[1] = "Pass";
                    return results;
                }
                else
                {
                    results[0] = "False";
                    results[1] = "Pass";
                    return results;
                }
            }
        }

        private void UserNotification()
        {
            foreach (Appointment a in Appointments)
            {
                if (a.UserId == User.UserId)
                {
                    if ((a.Start > DateTime.UtcNow) && (a.Start <= DateTime.UtcNow.AddMinutes(15)))
                    {
                        MessageBox.Show(User.UserName + " has an appointment in the next " + a.Start.Subtract(DateTime.UtcNow).ToString("mm") + " minutes.", this.Text);
                        break;
                    }
                }
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ButtonAll_Click(object sender, EventArgs e)
        {
            //  Reset DGV to all appointments.
            UpdateAppointments();
        }

        private void ButtonWeek_Click(object sender, EventArgs e)
        {
            //  Set DGV to current weeks appointments.
            UpdateAppointments();
            DateTime start = DateTime.UtcNow;
            DateTime end = DateTime.UtcNow;
            switch (DateTime.UtcNow.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    start = DateTime.UtcNow;
                    end = DateTime.UtcNow.AddDays(+6);
                    break;
                case DayOfWeek.Monday:
                    start = DateTime.UtcNow.AddDays(-1);
                    end = DateTime.UtcNow.AddDays(+5);
                    break;
                case DayOfWeek.Tuesday:
                    start = DateTime.UtcNow.AddDays(-2);
                    end = DateTime.UtcNow.AddDays(+4);
                    break;
                case DayOfWeek.Wednesday:
                    start = DateTime.UtcNow.AddDays(-3);
                    end = DateTime.UtcNow.AddDays(+3);
                    break;
                case DayOfWeek.Thursday:                    
                    start = DateTime.UtcNow.AddDays(-4);
                    end = DateTime.UtcNow.AddDays(+2);
                    break;
                case DayOfWeek.Friday:
                    start = DateTime.UtcNow.AddDays(-5);
                    end = DateTime.UtcNow.AddDays(+1);
                    break;
                case DayOfWeek.Saturday:
                    start = DateTime.UtcNow.AddDays(-6);
                    end = DateTime.UtcNow;
                    break;
            }
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("Start > '{0}' AND Start < '{1}' AND [User Id] = {2}",start,end,User.UserId);
        }

        private void ButtonMonth_Click(object sender, EventArgs e)
        {
            //  Set DGV to current month.
            UpdateAppointments();
            DateTime start = DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day);
            DateTime end = DateTime.UtcNow.AddDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) - DateTime.UtcNow.Day);
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("Start > '{0}' AND Start < '{1}' AND [User Id] = {2}", start, end,User.UserId);
        }

        private void ButtonCust_Click(object sender, EventArgs e)
        {
            //  Open Customer form.
            if (Customer == null || Customer.IsDisposed)
            {
                Customer = new Customers(this);
                Customer.Show();
            }
            else
            {
                Customer.BringToFront();
            }
        }

        private void ButtonApt_Click(object sender, EventArgs e)
        {
            //  Open Appointment form.
            if (Aptmnts == null || Aptmnts.IsDisposed)
            {
                Aptmnts = new Appointments(this);
                Aptmnts.Show();
            }
            else
            {
                Aptmnts.BringToFront();
            }
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ButtonMonthly_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = ConnectToDB();
            connection.Open();            
            string sql = "SELECT YEAR(START) Year,MONTHNAME(START) Month, type Type,COUNT(*) Count from appointment GROUP BY YEAR(START),MONTHNAME(START),TYPE";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataAdapter rdr = new MySqlDataAdapter(cmd);
            rdr.Fill(dataTable);
            rdr.Dispose();
            connection.Close();
            dataGridView1.DataSource = dataTable;
        }

        private void ButtonConsult_Click(object sender, EventArgs e)
        {
            UpdateAppointments();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
            (dataGridView1.DataSource as DataTable).DefaultView.Sort = "User Name ASC,Start ASC";
        }

        private void ButtonCusts_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = ConnectToDB();
            connection.Open();
            string sql = "Select customerName Customer, type Type,COUNT(*) Appointments from appointment join customer on appointment.customerId = customer.customerId GROUP BY Customer, Type";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataAdapter rdr = new MySqlDataAdapter(cmd);
            rdr.Fill(dataTable);
            rdr.Dispose();
            connection.Close();
            dataGridView1.DataSource = dataTable;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            //  Open Appointment form.
            if (Search == null || Search.IsDisposed)
            {
                Search = new Search(this);
                Search.Show();
            }
            else
            {
                Search.BringToFront();
            }
        }
    }
    //  End of Class
}