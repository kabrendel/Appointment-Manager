using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Appointment_Scheduler
{
    public class DataTables
    {
        private readonly DBObjects DBObject;
        public DataTables(DBObjects objects)
        {
            DBObject = objects;
        }
        public DataTable CustomerList()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            foreach (Customer c in DBObject.GetCustomerList())
            {
                DataRow row = dataTable.NewRow();
                row["Name"] = c.CustomerName;
                row["Id"] = c.CustomerId;
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        public List<string> TypeList()
        {
            return _ = new List<string>
            {
                "Test",
                "Scrum",
                "Presentation"
            };
        }
        public DataTable UserList(bool all,int userId)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("ID", typeof(int));
            foreach (User u in DBObject.GetUserList())
            {
                DataRow row = dataTable.NewRow();
                row["Name"] = u.UserName;
                row["Id"] = u.UserId;
                dataTable.Rows.Add(row);
            }
            if (!all)
            {
                //  Filter to just the logged in users appointments.
                dataTable.DefaultView.RowFilter = String.Format("[Id] = {0}", userId);
            }
            return dataTable;
        }
        public DataTable BuildAppointmentTable(Tuple<bool,int> user)
        {
            BindingList<Appointment> appointments;
            if (user.Item1)
            {
                appointments = DBObject.GetAppointments();
            }
            else
            {
                appointments = DBObject.GetAppointments(user.Item2);
            }
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
            foreach (Appointment a in appointments)
            {
                DataRow row = dataTable.NewRow();
                row["Appointment Id"] = a.AppointmentId;
                row["Title"] = a.Title;
                row["Start"] = a.Start;
                row["End"] = a.End;
                row["Type"] = a.Type;
                foreach (Customer c in DBObject.GetCustomers())
                {
                    if (a.CustomerId == c.CustomerId)
                    {
                        row["Customer Id"] = c.CustomerId;
                        row["Customer Name"] = c.CustomerName;
                        break;
                    }
                }
                foreach (User u in DBObject.GetUsers())
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
            DBObjects db = new DBObjects();
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
            foreach (Customer c in db.GetCustomers())
            {
                DataRow row = dataTable.NewRow();
                row["Customer Id"] = c.CustomerId;
                row["Customer Name"] = c.CustomerName;
                foreach (Address a in db.GetAddresses())
                {
                    if (a.AddressId == c.AddressId)
                    {
                        row["Address Id"] = a.AddressId;
                        row["Address1"] = a.Address1;
                        row["Address2"] = a.Address2;
                        row["Postal Code"] = a.PostalCode;
                        row["Phone Number"] = a.Phone;
                        foreach (City i in db.GetCities())
                        {
                            if (i.CityId == a.CityId)
                            {
                                row["City Id"] = i.CityId;
                                row["City"] = i.ACity;
                                foreach (Country y in db.GetCountries())
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
    }//End of class
}