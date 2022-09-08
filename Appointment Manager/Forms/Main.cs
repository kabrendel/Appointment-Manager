using System;
using System.Data;
using System.Windows.Forms;

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
        public SQLQueries SQLFunctions; //  shouldn't be in main but used by other winforms
        //
        public Repository Repo;
        //
        public Main()
        {
            InitializeComponent();
            DBObject = new DBObjects();
            DBObject.LoadUsers();
            DBObject.LoadCustomers();
            DTBuilder = new DataTables(this, DBObject);
            SQLFunctions = new SQLQueries(this, DBObject);
            Repo = new Repository();
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
        public void UpdateAppointments()
        {
            //  not sure I like this method.
            AppointmentsGridView.DataSource = null;
            AppointmentsGridView.DataSource = DTBuilder.BuildAppointmentTable();
            AppointmentsGridView.Columns[0].Visible = false;
            AppointmentsGridView.Columns[2].Visible = false;
            AppointmentsGridView.Columns[4].Visible = false;
        }
        public string[] UserLogin(string user, string pass)
        {
            //  User object.
            if (User != null)
            {
                // Reset user object created below.
                User = null;
            }
            //  Result to pass back to Login form but this code should probably move to Login form out of Main.
            string[] results = new string[2];
            //  Temporarily store user password for comparison, only time we touch a password.
            string userPass = Repo.UserPassword(user);
            User = Repo.UserObject(user);
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
        //  Events
        private void AppointmentsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AppointmentsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        //  Open new forms
        private void ButtonCust_Click(object sender, EventArgs e)
        {
            //  Open Customer form.
            if (Customer?.IsDisposed != false)
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
            if (Aptmnts?.IsDisposed != false)
            {
                Aptmnts = new Appointments(this);
                Aptmnts.Show();
            }
            else
            {
                Aptmnts.BringToFront();
            }
        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            //  Open Search form.
            if (Search?.IsDisposed != false)
            {
                Search = new Search(this);
                Search.Show();
            }
            else
            {
                Search.BringToFront();
            }
        }
        //  Buttons for "reports"
        private void ButtonMonthly_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = Repo.MonthlyReport();
        }
        private void ButtonConsultants_Click(object sender, EventArgs e)
        {
            DBObject.LoadAppointments();
            UpdateAppointments();
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = null;
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .Sort = "User Name ASC,Start ASC";
        }
        private void ButtonCustomers_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = Repo.CustomerReport();
        }
        //  Buttons for logged in user "reports".
        private void ButtonAll_Click(object sender, EventArgs e)
        {
            //  Set DGV to all appointments for logged in user.
            DBObject.LoadAppointments(User.UserId);
            UpdateAppointments();
        }
        private void ButtonWeek_Click(object sender, EventArgs e)
        {
            //  Set DGV to current weeks appointments.
            DBObject.LoadAppointments(User.UserId);
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
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("Start > '{0}' AND Start < '{1}'", start, end);
        }
        private void ButtonMonth_Click(object sender, EventArgs e)
        {
            //  Set DGV to current month.
            DBObject.LoadAppointments(User.UserId);
            UpdateAppointments();
            DateTime start = DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day);
            DateTime end = DateTime.UtcNow.AddDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) - DateTime.UtcNow.Day);
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("Start > '{0}' AND Start < '{1}'", start, end);
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }//  End of Class
}