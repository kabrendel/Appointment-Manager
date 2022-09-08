using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public class Repository
    {
        //  Repository class for domain layer
        //  repository should create a DBObjects object
        //  datatables??
        private DBObjects _dbObjects;
        private DataTables _dataTables;
        private User _user;
        //  Constructor
        public Repository()
        {
            _dbObjects = new DBObjects();
            _dataTables = new DataTables(_dbObjects);
        }
        //
        public string UserPassword(string user)
        {
            return _dbObjects.UserPassword(user);
        }
        public User UserObject(string user)
        {
            return _dbObjects.UserObject(user);
        }
        //
        public DataTable CustomerReport()
        {
            return _dataTables.CustomerReport();
        }
        public DataTable MonthlyReport()
        {
            return _dataTables.MonthlyReport();
        }
        public DataTable GetAppointmentTable()
        {
            return _dataTables.BuildAppointmentTable(_dbObjects.GetAppointments(_user.UserId),_dbObjects.GetCustomers(),_dbObjects.GetUsers());
        }
        public DataTable GetAppointmentTableAll()
        {
            return _dataTables.BuildAppointmentTable(_dbObjects.GetAppointments(), _dbObjects.GetCustomers(), _dbObjects.GetUsers());
        }
        internal void SetUser(User user)
        {
            // replace later..
            _user = user;
        }
        internal BindingList<Appointment> GetUserAppointments()
        {
            return _dbObjects.GetAppointments();
        }
        //
    }// End of class.
}
