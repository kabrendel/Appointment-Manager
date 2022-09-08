using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public class DataTables
    {
        readonly Main main;
        private readonly DBObjects DBObject;
        public DataTables(Main main,DBObjects objects)
        {
            this.main = main;
            DBObject = objects;
        }
        public DataTables(DBObjects objects)
        {
            DBObject = objects;
        }
        //  list builders
        public DataTable CustomerList()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            const string sqlString = "SELECT customerName,customerId FROM customer";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DataRow row = dataTable.NewRow();
                            row["Name"] = rdr[0].ToString();
                            row["ID"] = int.Parse(rdr[1].ToString());
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            return dataTable;
        }
        public List<string> TypeList()
        {
            List<string> list = new List<string>
            {
                "Test",
                "Scrum",
                "Presentation"
            };
            return list;
        }
        public DataTable UserList(bool all)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            const string sqlString = "SELECT userName,userId FROM user";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DataRow row = dataTable.NewRow();
                            row["Name"] = rdr[0].ToString();
                            row["ID"] = int.Parse(rdr[1].ToString());
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            if (!all)
            {
                //  Filter to just the logged in users appointments.
                dataTable.DefaultView.RowFilter = String.Format("[Id] = {0}", main.User.UserId);
            }
            return dataTable;
        }
        // table builders
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
            foreach (Appointment a in DBObject.Appointments)
            {
                DataRow row = dataTable.NewRow();
                row["Appointment Id"] = a.AppointmentId;
                row["Title"] = a.Title;
                row["Start"] = a.Start;
                row["End"] = a.End;
                row["Type"] = a.Type;
                foreach (Customer c in DBObject.Customers)
                {
                    if (a.CustomerId == c.CustomerId)
                    {
                        row["Customer Id"] = c.CustomerId;
                        row["Customer Name"] = c.CustomerName;
                        break;
                    }
                }
                foreach (User u in DBObject.Users)
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
            foreach (Customer c in DBObject.Customers)
            {
                DataRow row = dataTable.NewRow();
                row["Customer Id"] = c.CustomerId;
                row["Customer Name"] = c.CustomerName;
                foreach (Address a in DBObject.Addresses)
                {
                    if (a.AddressId == c.AddressId)
                    {
                        row["Address Id"] = a.AddressId;
                        row["Address1"] = a.Address1;
                        row["Address2"] = a.Address2;
                        row["Postal Code"] = a.PostalCode;
                        row["Phone Number"] = a.Phone;
                        foreach (City i in DBObject.Cities)
                        {
                            if (i.CityId == a.CityId)
                            {
                                row["City Id"] = i.CityId;
                                row["City"] = i.ACity;
                                foreach (Country y in DBObject.Countries)
                                {
                                    if (y.CountryId == i.CountryId)
                                    {
                                        row["Country Id"] = y.CountryId;
                                        row["Country"] = y.ACountry;
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
            blank["Customer Id"] = 0;
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

        public DataTable CustomerReport()
        {
            DataTable dt = new DataTable();
            const string sqlString = "Select customerName Customer, type Type,COUNT(*) Appointments from appointment join customer on appointment.customerId = customer.customerId GROUP BY Customer, Type";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    try
                    {
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving customer report: " + '\n' + ex, "Temp");
                    }
                }
            }
            return null;
        }

        public DataTable MonthlyReport()
        {
            DataTable dt = new DataTable();
            const string sqlString = "SELECT YEAR(START) Year,MONTHNAME(START) Month, type Type,COUNT(*) Count from appointment GROUP BY YEAR(START),MONTHNAME(START),TYPE";
            using (var connection2 = Connection.CreateAndOpen())
            {
                using (MySqlCommand cmd = new MySqlCommand(sqlString, connection2))
                {
                    try
                    {
                        dt.Load(cmd.ExecuteReader());
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving monthly report: " + '\n' + ex, "Temp");
                    }
                }
            }
            return null;
        }
    }//End of class
}
