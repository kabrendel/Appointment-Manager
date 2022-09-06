using System;
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
        //  Logged in User object.
        public User User;
        //  Objects for methods.
        public DBObjects DBObject;
        public DataTables DTBuilder;
        //
        public Main()
        {
            InitializeComponent();
            DBObject = new DBObjects();
            DBObject.LoadUsers();
            DBObject.LoadCustomers();
            DTBuilder = new DataTables(this,DBObject);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            Login = new Login(this);
            var confirm = Login.ShowDialog();
            if (confirm == DialogResult.OK)
            {
                DBObject.LoadAppointments(User.UserId);
                UserNotifications();
                UpdateAppointments();
            }
        }
        public void UpdateAppointments()
        {
            //  rename? possibly move to DataTables
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DTBuilder.BuildAppointmentTable();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            (dataGridView1.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("[User Id] = {0}", User.UserId);
        }

        public bool CreateAppointment(int customerId, int userId, string type, DateTime start, DateTime end)
        {
            const string title = "not needed";
            const string desc = "not needed";
            const string location = "not needed";
            const string contact = "not needed";
            const string url = "not needed";
            const string sqlCommand = "INSERT INTO appointment VALUES (null,@customer,@user,@title,@desc,@loc,@cont,@type,@url,@start,@end,@time1,@user1,@time2,@user2)";
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, CNObject.connection);
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
                CNObject.ConnectionClose();
                return true;
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
                MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool UpdateAppointment(int appointmentId,int customerId, int userId, string type, DateTime start, DateTime end)
        {
            const string title = "not needed";
            const string desc = "not needed";
            const string location = "not needed";
            const string contact = "not needed";
            const string url = "not needed";
            const string sqlCommand = "UPDATE appointment SET customerId = @customer, userId = @user,type = @type,start = @start,end = @end,lastUpdate = @time2,lastUpdateBy = @user2 WHERE appointmentId = @appointmentId";

            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, CNObject.connection);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                cmd.Parameters.AddWithValue("@customer", customerId);
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@start", start.ToUniversalTime());
                cmd.Parameters.AddWithValue("@end", end.ToUniversalTime());
                cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user2", User.UserName);
                cmd.ExecuteNonQuery();
                foreach (Appointment apt in DBObject.Appointments.Where(x => x.AppointmentId == appointmentId).ToList()) DBObject.Appointments.Remove(apt);
                DBObject.NewAppointment(appointmentId, customerId, userId, title, desc, location, contact, type, url, start, end, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);
                CNObject.ConnectionClose();
                return true;
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
                MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool RemoveAppointment(int appointmentId)
        {
            const string sqlCommand = "DELETE FROM appointment WHERE appointmentId=@appointmentId";
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(sqlCommand, CNObject.connection);
                cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                cmd.ExecuteNonQuery();
                foreach (Appointment apt in DBObject.Appointments.Where(x => x.AppointmentId == appointmentId).ToList()) DBObject.Appointments.Remove(apt);
                CNObject.ConnectionClose();
                return true;
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
                MessageBox.Show("Error removing records from database: " + '\n' + ex, this.Text);
                return false;
            }
        }
        public bool AddCustomer(int customerId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            if (customerId != 0)
            {
                //  testing...
                return false;
            }
            else
            {
                const string countryString = "INSERT INTO country VALUES (null,@country,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string cityString = "INSERT INTO city VALUES (null,@city,@countryId,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string addrString = "INSERT INTO address values (null,@ad1,@ad2,@cityId,@postal,@phone,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string custString = "INSERT INTO customer VALUES (null,@name,@addrId,@bool,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                Connection CNObject = new Connection();
                CNObject.CreateConnection();
                CNObject.ConnectionOpen();
                MySqlCommand cmd;
                try
                {
                    cmd = new MySqlCommand(countryString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    var countryIndex = cmd.ExecuteScalar();
                    DBObject.NewCountry(int.Parse(countryIndex.ToString()),country,DateTime.UtcNow,User.UserName,DateTime.UtcNow,User.UserName);

                    cmd = new MySqlCommand(cityString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@countryId", countryIndex);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    var cityIndex = cmd.ExecuteScalar();
                    DBObject.NewCity(int.Parse(cityIndex.ToString()), city, int.Parse(countryIndex.ToString()), DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                    cmd = new MySqlCommand(addrString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@ad1", ad1);
                    cmd.Parameters.AddWithValue("@ad2", ad2);
                    cmd.Parameters.AddWithValue("@cityId", cityIndex);
                    cmd.Parameters.AddWithValue("@postal", postal);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    var addressIndex = cmd.ExecuteScalar();
                    DBObject.NewAddr(int.Parse(addressIndex.ToString()),ad1,ad2, int.Parse(cityIndex.ToString()), postal,phone,DateTime.UtcNow,User.UserName,DateTime.UtcNow,User.UserName);

                    cmd = new MySqlCommand(custString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@addrId", addressIndex);
                    cmd.Parameters.AddWithValue("@bool", true);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", User.UserName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", User.UserName);
                    var customerIndex = cmd.ExecuteScalar();
                    DBObject.NewCustomer(int.Parse(customerIndex.ToString()),name, int.Parse(addressIndex.ToString()), true,DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                    CNObject.ConnectionClose();
                    return true;
                }
                catch (Exception ex)
                {
                    CNObject.ConnectionClose();
                    MessageBox.Show("Error adding records to database: " + '\n' + ex, this.Text);
                    return false;
                }
            }
        }

        public bool UpdateCustomer(int customerId, int addressId, int cityId, int countryId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            const string countryString = "UPDATE country SET country = @country, lastUpdate = @time1, lastUpdateBy = @user1 WHERE countryid = @countryId";
            const string cityString = "UPDATE city SET city = @city, countryId = @countryId, lastUpdate = @time1, lastUpdateBy = @user1 WHERE cityid = @cityId";
            const string addrString = "UPDATE address SET address = @ad1, address2 = @ad2, cityId = @cityId, postalCode = @postal, phone = @phone, lastUpdate = @time1, lastUpdateBy = @user1 WHERE addressId = @addrId";
            const string custString = "UPDATE customer SET customerName = @name, addressId = @addrId, active = @bool, lastUpdate = @time1, lastUpdateBy = @user1 WHERE customerId = @customerId";

            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            MySqlCommand cmd;
            try
            {
                cmd = new MySqlCommand(countryString, CNObject.connection);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.ExecuteNonQuery();
                //  Lambda expression to find our object and remove the old one from our collection.
                foreach (Country c in DBObject.Countries.Where(x => x.CountryId == countryId).ToList()) DBObject.Countries.Remove(c);
                DBObject.NewCountry(countryId, country, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(cityString, CNObject.connection);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.ExecuteNonQuery();
                foreach (City c in DBObject.Cities.Where(x => x.CityId == cityId).ToList()) DBObject.Cities.Remove(c);
                DBObject.NewCity(cityId, city, countryId, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(addrString, CNObject.connection);
                cmd.Parameters.AddWithValue("@ad1", ad1);
                cmd.Parameters.AddWithValue("@ad2", ad2);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.Parameters.AddWithValue("@postal", postal);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@addrId", addressId);
                cmd.ExecuteNonQuery();
                foreach (Address a in DBObject.Addresses.Where(x => x.AddressId == addressId).ToList()) DBObject.Addresses.Remove(a);
                DBObject.NewAddr(addressId, ad1, ad2, cityId, postal, phone, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                cmd = new MySqlCommand(custString, CNObject.connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@addrId", addressId);
                cmd.Parameters.AddWithValue("@bool", true);
                cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@user1", User.UserName);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.ExecuteNonQuery();
                foreach (Customer c in DBObject.Customers.Where(x => x.CustomerId == customerId).ToList()) DBObject.Customers.Remove(c);
                DBObject.NewCustomer(customerId, name, addressId, true, DateTime.UtcNow, User.UserName, DateTime.UtcNow, User.UserName);

                CNObject.ConnectionClose();
                return true;
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
                MessageBox.Show("Error updating records in database: " + '\n' + ex, this.Text);
                return false;
            }
        }

        public bool DeleteCustomer(int cust, int addr, int city, int cntry)
        {
            const string countryString = "DELETE FROM country WHERE countryId=@countryId";
            const string cityString = "DELETE FROM city WHERE cityId=@cityId";
            const string addrString = "DELETE FROM address WHERE addressId=@addressId";
            const string custString = "DELETE FROM customer WHERE customerId=@customerId";

            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            MySqlCommand cmd;
            object reader;
            try
            {
                // check for appointments, unable to delete a customer with appointments.
                const string custcheck = "select customerId from appointment where customerId=@cust";
                cmd = new MySqlCommand(custcheck, CNObject.connection);
                cmd.Parameters.AddWithValue("@cust", cust);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    //  No rows returned from appointment table, safe to delete customer.
                    cmd = new MySqlCommand(custString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@customerId", cust);
                    cmd.ExecuteNonQuery();
                    foreach (Customer c in DBObject.Customers.Where(x => x.CustomerId == cust).ToList()) DBObject.Customers.Remove(c);
                }
                else
                {
                    // prompt to delete appointments?
                    var confirmDelete = MessageBox.Show("Customer has scheduled appointments, delete appointments?", this.Text, MessageBoxButtons.OKCancel);
                    if (confirmDelete == DialogResult.OK)
                    {
                        //  remove appointments from table
                        const string delapts = "delete from appointment where customerId=@cust";
                        cmd = new MySqlCommand(delapts, CNObject.connection);
                        cmd.Parameters.AddWithValue("@cust", cust);
                        cmd.ExecuteNonQuery();
                        foreach (Appointment apt in DBObject.Appointments.Where(x => x.CustomerId == cust).ToList()) DBObject.Appointments.Remove(apt);
                        //  remove customer
                        cmd = new MySqlCommand(custString, CNObject.connection);
                        cmd.Parameters.AddWithValue("@customerId", cust);
                        cmd.ExecuteNonQuery();
                        foreach (Customer c in DBObject.Customers.Where(x => x.CustomerId == cust).ToList()) DBObject.Customers.Remove(c);
                    }
                    else
                    {
                        return false;
                    }
                }
                // remove address if no associated customer reference
                const string addrcheck = "select addressId from customer where addressId=@addr";
                cmd = new MySqlCommand(addrcheck, CNObject.connection);
                cmd.Parameters.AddWithValue("@addr", addr);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    //  will be null if no rows in table match.
                    cmd = new MySqlCommand(addrString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@addressId", addr);
                    cmd.ExecuteNonQuery();
                    foreach (Address a in DBObject.Addresses.Where(x => x.AddressId == addr).ToList()) DBObject.Addresses.Remove(a);
                }
                // remove city if no associated address reference
                const string citycheck = "select cityId from address where cityId=@city";
                cmd = new MySqlCommand(citycheck, CNObject.connection);
                cmd.Parameters.AddWithValue("@city", city);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    cmd = new MySqlCommand(cityString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@cityId", city);
                    cmd.ExecuteNonQuery();
                    foreach (City c in DBObject.Cities.Where(x => x.CityId == city).ToList()) DBObject.Cities.Remove(c);
                }
                // remove country if no associated city reference
                const string cntrycheck = "select countryId from city where countryId=@cntry";
                cmd = new MySqlCommand(cntrycheck, CNObject.connection);
                cmd.Parameters.AddWithValue("@cntry", cntry);
                reader = cmd.ExecuteScalar();
                if (reader == null)
                {
                    cmd = new MySqlCommand(countryString, CNObject.connection);
                    cmd.Parameters.AddWithValue("@countryId", cntry);
                    cmd.ExecuteNonQuery();
                    foreach (Country c in DBObject.Countries.Where(x => x.CountryId == cntry).ToList()) DBObject.Countries.Remove(c);
                }
                //
                CNObject.ConnectionClose();
                return true;
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
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
            //  Result to pass back to caller.
            string[] results = new string[2];
            //  Temporary store user password for comparison.
            string userPass = null;
            //  DB things
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            //const string sqlString = "SELECT * FROM user WHERE userName = @user";
            string sqlString = "SELECT password FROM user WHERE userName = @user";
            MySqlCommand cmd = new MySqlCommand(sqlString, CNObject.connection);
            cmd.Parameters.AddWithValue("@user", user);
            try
            {
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userPass = rdr[0].ToString();
                }
                rdr.Close();
                sqlString = "SELECT userId, userName, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM user WHERE userName = @user";
                cmd = new MySqlCommand(sqlString, CNObject.connection);
                cmd.Parameters.AddWithValue("@user", user);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    User = DBObject.NewUser(rdr);
                }
                rdr.Close();
                CNObject.ConnectionClose();
            }
            catch (Exception ex)
            {
                CNObject.ConnectionClose();
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
                if (userPass == pass)
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

        private void UserNotifications()
        {
            foreach (Appointment a in DBObject.Appointments)
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
            DBObject.LoadAppointments();
            UpdateAppointments();
        }

        private void ButtonWeek_Click(object sender, EventArgs e)
        {
            //  Set DGV to current weeks appointments.
            DBObject.LoadAppointments();
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
            DBObject.LoadAppointments();
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
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            string sql = "SELECT YEAR(START) Year,MONTHNAME(START) Month, type Type,COUNT(*) Count from appointment GROUP BY YEAR(START),MONTHNAME(START),TYPE";
            MySqlDataAdapter rdr = CNObject.SQLAdapter(sql);
            rdr.Fill(dataTable);
            rdr.Dispose();
            CNObject.ConnectionClose();
            dataGridView1.DataSource = dataTable;
        }

        private void ButtonConsult_Click(object sender, EventArgs e)
        {
            DBObject.LoadAppointments();
            UpdateAppointments();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = null;
            (dataGridView1.DataSource as DataTable).DefaultView.Sort = "User Name ASC,Start ASC";
        }

        private void ButtonCusts_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataTable dataTable = new DataTable();
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            string sql = "Select customerName Customer, type Type,COUNT(*) Appointments from appointment join customer on appointment.customerId = customer.customerId GROUP BY Customer, Type";
            MySqlDataAdapter rdr = CNObject.SQLAdapter(sql);
            rdr.Fill(dataTable);
            rdr.Dispose();
            CNObject.ConnectionClose();
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
    }//  End of Class
}