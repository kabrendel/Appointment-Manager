using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public class SQLQueries
    {
        //private readonly Repository Repo;
        private readonly string windowText = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Database"].ToString();
        private readonly string userName = ConfigurationManager.AppSettings["userid"];
        //  Constructor
        public SQLQueries()
        {
            //Repo = new Repository();
        }
        //  Connection Method
        private MySqlConnection CreateAndOpen()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        //  Insert, Update, Delete, methods.
        public bool CreateAppointment(int customerId, int userId, string type, DateTime start, DateTime end)
        {
            const string title = "not needed";
            const string desc = "not needed";
            const string location = "not needed";
            const string contact = "not needed";
            const string url = "not needed";
            const string sqlString = "INSERT INTO appointment VALUES (null,@customer,@user,@title,@desc,@loc,@cont,@type,@url,@start,@end,@time1,@user1,@time2,@user2)";
            using (var connection = CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
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
                    cmd.Parameters.AddWithValue("@user1", userName);
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", userName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding records to database: " + '\n' + ex, windowText);
                        return false;
                    }
                }
            }
        }
        public bool UpdateAppointment(int appointmentId, int customerId, int userId, string type, DateTime start, DateTime end)
        {
            const string sqlString = "UPDATE appointment SET customerId = @customer, userId = @user,type = @type,start = @start,end = @end,lastUpdate = @time2,lastUpdateBy = @user2 WHERE appointmentId = @appointmentId";
            using (var connection = CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    cmd.Parameters.AddWithValue("@customer", customerId);
                    cmd.Parameters.AddWithValue("@user", userId);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@start", start.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@end", end.ToUniversalTime());
                    cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user2", userName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding records to database: " + '\n' + ex, windowText);
                        return false;
                    }
                }
            }
        }
        public bool RemoveAppointment(int appointmentId)
        {
            const string sqlString = "DELETE FROM appointment WHERE appointmentId=@appointmentId";
            using (var connection = CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error removing records from database: " + '\n' + ex, windowText);
                        return false;
                    }
                }
            }
        }
        public bool AddCustomer(int customerId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            if (customerId != 0)
            {
                return false;
            }
            else
            {
                const string countryString = "INSERT INTO country VALUES (null,@country,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string cityString = "INSERT INTO city VALUES (null,@city,@countryId,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string addrString = "INSERT INTO address values (null,@ad1,@ad2,@cityId,@postal,@phone,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";
                const string custString = "INSERT INTO customer VALUES (null,@name,@addrId,@bool,@time1,@user1,@time2,@user2); SELECT LAST_INSERT_ID();";

                using (var connection = CreateAndOpen())
                {
                    MySqlCommand cmd;
                    try
                    {
                        cmd = new MySqlCommand(countryString, connection);
                        cmd.Parameters.AddWithValue("@country", country);
                        cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user1", userName);
                        cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user2", userName);
                        var countryIndex = cmd.ExecuteScalar();

                        cmd = new MySqlCommand(cityString, connection);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@countryId", countryIndex);
                        cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user1", userName);
                        cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user2", userName);
                        var cityIndex = cmd.ExecuteScalar();

                        cmd = new MySqlCommand(addrString, connection);
                        cmd.Parameters.AddWithValue("@ad1", ad1);
                        cmd.Parameters.AddWithValue("@ad2", ad2);
                        cmd.Parameters.AddWithValue("@cityId", cityIndex);
                        cmd.Parameters.AddWithValue("@postal", postal);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user1", userName);
                        cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user2", userName);
                        var addressIndex = cmd.ExecuteScalar();

                        cmd = new MySqlCommand(custString, connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@addrId", addressIndex);
                        cmd.Parameters.AddWithValue("@bool", true);
                        cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user1", userName);
                        cmd.Parameters.AddWithValue("@time2", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@user2", userName);
                        var customerIndex = cmd.ExecuteScalar();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding records to database: " + '\n' + ex, windowText);
                        return false;
                    }
                }
            }
        }
        public bool UpdateCustomer(int customerId, int addressId, int cityId, int countryId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            const string countryString = "UPDATE country SET country = @country, lastUpdate = @time1, lastUpdateBy = @user1 WHERE countryid = @countryId";
            const string cityString = "UPDATE city SET city = @city, countryId = @countryId, lastUpdate = @time1, lastUpdateBy = @user1 WHERE cityid = @cityId";
            const string addrString = "UPDATE address SET address = @ad1, address2 = @ad2, cityId = @cityId, postalCode = @postal, phone = @phone, lastUpdate = @time1, lastUpdateBy = @user1 WHERE addressId = @addrId";
            const string custString = "UPDATE customer SET customerName = @name, addressId = @addrId, active = @bool, lastUpdate = @time1, lastUpdateBy = @user1 WHERE customerId = @customerId";
            using (var connection = CreateAndOpen())
            {
                MySqlCommand cmd;
                try
                {
                    cmd = new MySqlCommand(countryString, connection);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", userName);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand(cityString, connection);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", userName);
                    cmd.Parameters.AddWithValue("@cityId", cityId);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand(addrString, connection);
                    cmd.Parameters.AddWithValue("@ad1", ad1);
                    cmd.Parameters.AddWithValue("@ad2", ad2);
                    cmd.Parameters.AddWithValue("@cityId", cityId);
                    cmd.Parameters.AddWithValue("@postal", postal);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", userName);
                    cmd.Parameters.AddWithValue("@addrId", addressId);
                    cmd.ExecuteNonQuery();

                    cmd = new MySqlCommand(custString, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@addrId", addressId);
                    cmd.Parameters.AddWithValue("@bool", true);
                    cmd.Parameters.AddWithValue("@time1", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@user1", userName);
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating records in database: " + '\n' + ex, windowText);
                    return false;
                }
            }
        }
        public bool DeleteCustomer(int cust, int addr, int city, int cntry)
        {
            const string countryString = "DELETE FROM country WHERE countryId=@countryId";
            const string cityString = "DELETE FROM city WHERE cityId=@cityId";
            const string addrString = "DELETE FROM address WHERE addressId=@addressId";
            const string custString = "DELETE FROM customer WHERE customerId=@customerId";
            using (var connection = CreateAndOpen())
            {
                MySqlCommand cmd;
                object reader;
                try
                {
                    // check for appointments, unable to delete a customer with appointments.
                    const string custcheck = "select customerId from appointment where customerId=@cust";
                    cmd = new MySqlCommand(custcheck, connection);
                    cmd.Parameters.AddWithValue("@cust", cust);
                    reader = cmd.ExecuteScalar();
                    if (reader == null)
                    {
                        //  No rows returned from appointment table, safe to delete customer.
                        cmd = new MySqlCommand(custString, connection);
                        cmd.Parameters.AddWithValue("@customerId", cust);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // prompt to delete appointments?
                        var confirmDelete = MessageBox.Show("Customer has scheduled appointments, delete appointments?", windowText, MessageBoxButtons.OKCancel);
                        if (confirmDelete == DialogResult.OK)
                        {
                            //  remove appointments from table
                            const string delapts = "delete from appointment where customerId=@cust";
                            cmd = new MySqlCommand(delapts, connection);
                            cmd.Parameters.AddWithValue("@cust", cust);
                            cmd.ExecuteNonQuery();
                            //  remove customer
                            cmd = new MySqlCommand(custString, connection);
                            cmd.Parameters.AddWithValue("@customerId", cust);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    // remove address if no associated customer reference
                    const string addrcheck = "select addressId from customer where addressId=@addr";
                    cmd = new MySqlCommand(addrcheck, connection);
                    cmd.Parameters.AddWithValue("@addr", addr);
                    reader = cmd.ExecuteScalar();
                    if (reader == null)
                    {
                        //  will be null if no rows in table match.
                        cmd = new MySqlCommand(addrString, connection);
                        cmd.Parameters.AddWithValue("@addressId", addr);
                        cmd.ExecuteNonQuery();
                    }
                    // remove city if no associated address reference
                    const string citycheck = "select cityId from address where cityId=@city";
                    cmd = new MySqlCommand(citycheck, connection);
                    cmd.Parameters.AddWithValue("@city", city);
                    reader = cmd.ExecuteScalar();
                    if (reader == null)
                    {
                        cmd = new MySqlCommand(cityString, connection);
                        cmd.Parameters.AddWithValue("@cityId", city);
                        cmd.ExecuteNonQuery();
                    }
                    // remove country if no associated city reference
                    const string cntrycheck = "select countryId from city where countryId=@cntry";
                    cmd = new MySqlCommand(cntrycheck, connection);
                    cmd.Parameters.AddWithValue("@cntry", cntry);
                    reader = cmd.ExecuteScalar();
                    if (reader == null)
                    {
                        cmd = new MySqlCommand(countryString, connection);
                        cmd.Parameters.AddWithValue("@countryId", cntry);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error removing records from database: " + '\n' + ex, windowText);
                    return false;
                }
            }
        }
    }// End of class
}