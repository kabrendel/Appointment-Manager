using System.Data;

namespace Appointment_Manager
{
    public class Repository
    {
        //  Repository class for domain layer
        //  repository should create a DBObjects object
        //  datatables??
        private DBObjects dbObjects;
        private DataTables dataTables;
        //  Constructor
        public Repository()
        {
            dbObjects = new DBObjects();
            dataTables = new DataTables(dbObjects);
        }
        //
        public string UserPassword(string user)
        {
            return dbObjects.UserPassword(user);
        }
        public User UserObject(string user)
        {
            return dbObjects.UserObject(user);
        }
        //
        public DataTable CustomerReport()
        {
            return dataTables.CustomerReport();
        }
        public DataTable MonthlyReport()
        {
            return dataTables.MonthlyReport();
        }
        //
    }// End of class.
}
