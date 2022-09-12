using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace Appointment_Scheduler
{
    internal class Repository
    {
        //  Statics
        static User User;
        //  Access Database and list/table builders.
        private readonly DBObjects _dbObjects;
        private readonly DataTables _dataTables;
        private readonly SQLQueries _sqlQueries;
        internal Repository()
        {
            _dbObjects = new DBObjects();
            _dataTables = new DataTables(_dbObjects);
            _sqlQueries = new SQLQueries();
        }
        #region DBObjects
        internal string GetUserPassword(string user)
        {
            return _dbObjects.UserPassword(user);
        }
        internal User GetUserObject(string user)
        {
            return _dbObjects.UserObject(user);
        }
        //
        internal DataTable CustomerReport()
        {
            return _dbObjects.CustomerReport();
        }
        internal DataTable MonthlyReport()
        {
            return _dbObjects.MonthlyReport();
        }
        internal BindingList<Appointment> GetUserAppointments()
        {
            return _dbObjects.GetAppointments(User.UserId);
        }
        #endregion
        #region DataTables
        internal DataTable GetAppointmentTable()
        {
            var user = Tuple.Create(false,User.UserId);
            return _dataTables.BuildAppointmentTable(user);
        }
        internal DataTable GetAppointmentTableAll()
        {
            var user = Tuple.Create(true, 0);
            return _dataTables.BuildAppointmentTable(user);
        }
        internal DataTable GetCustomerTable()
        {
            return _dataTables.BuildCustomerTable();
        }
        internal List<string> GetTypeList()
        {
            return _dataTables.TypeList();
        }
        internal DataTable GetCustomerList()
        {
            return _dataTables.CustomerList();
        }
        internal DataTable GetUserList(bool all)
        {
            return _dataTables.UserList(all, User.UserId);
        }
        #endregion
        #region SQLQueries
        internal bool CreateAppointment(int customerId, int userId, string type, DateTime start, DateTime end)
        {
            return _sqlQueries.CreateAppointment(customerId, userId, type, start, end);
        }
        internal bool UpdateAppointment(int appointmentId, int customerId, int userId, string type, DateTime start, DateTime end)
        {
            return _sqlQueries.UpdateAppointment(appointmentId, customerId, userId, type, start, end);
        }
        internal bool RemoveAppointment(int appointmentId)
        {
            return _sqlQueries.RemoveAppointment(appointmentId);
        }
        internal bool AddCustomer(int customerId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            return _sqlQueries.AddCustomer(customerId, name, ad1, ad2,  city,  postal,  country,  phone);
        }
        internal bool UpdateCustomer(int customerId, int addressId, int cityId, int countryId, string name, string ad1, string ad2, string city, string postal, string country, string phone)
        {
            return _sqlQueries.UpdateCustomer(customerId, addressId, cityId, countryId, name, ad1, ad2, city, postal, country, phone);
        }
        internal bool DeleteCustomer(int cust, int addr, int city, int cntry)
        {
            return _sqlQueries.DeleteCustomer(cust, addr, city, cntry);
        }
        #endregion
        internal void SetUser(User user)
        {
            User = user;
        }
        internal string GetUserName()
        {
            return User.UserName;
        }
    }// End of class.
}