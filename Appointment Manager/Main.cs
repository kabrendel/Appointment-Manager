using System;
using System.Data;
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
        public SQLQueries SQLFunctions;
        //
        public Main()
        {
            InitializeComponent();
            DBObject = new DBObjects();
            DBObject.LoadUsers();
            DBObject.LoadCustomers();
            DTBuilder = new DataTables(this,DBObject);
            SQLFunctions = new SQLQueries(this, DBObject);
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
            AppointmentsGridView.DataSource = null;
            AppointmentsGridView.DataSource = DTBuilder.BuildAppointmentTable();
            AppointmentsGridView.Columns[0].Visible = false;
            AppointmentsGridView.Columns[2].Visible = false;
            AppointmentsGridView.Columns[4].Visible = false;
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("[User Id] = {0}", User.UserId);
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
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("Start > '{0}' AND Start < '{1}' AND [User Id] = {2}", start, end, User.UserId);

        }

        private void ButtonMonth_Click(object sender, EventArgs e)
        {
            //  Set DGV to current month.
            DBObject.LoadAppointments();
            UpdateAppointments();
            DateTime start = DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day);
            DateTime end = DateTime.UtcNow.AddDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) - DateTime.UtcNow.Day);
            (AppointmentsGridView.DataSource as DataTable)
                .DefaultView
                .RowFilter = String.Format("Start > '{0}' AND Start < '{1}' AND [User Id] = {2}", start, end,User.UserId);
        }

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

        private void AppointmentsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AppointmentsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ButtonMonthly_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = null;
            DataTable dataTable = new DataTable();
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            const string sql = "SELECT YEAR(START) Year,MONTHNAME(START) Month, type Type,COUNT(*) Count from appointment GROUP BY YEAR(START),MONTHNAME(START),TYPE";
            MySqlDataAdapter rdr = CNObject.SQLAdapter(sql);
            rdr.Fill(dataTable);
            rdr.Dispose();
            CNObject.ConnectionClose();
            AppointmentsGridView.DataSource = dataTable;
        }

        private void ButtonConsult_Click(object sender, EventArgs e)
        {
            DBObject.LoadAppointments();
            UpdateAppointments();
            (AppointmentsGridView.DataSource as DataTable).DefaultView.RowFilter = null;
            (AppointmentsGridView.DataSource as DataTable).DefaultView.Sort = "User Name ASC,Start ASC";
        }

        private void ButtonCusts_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = null;
            DataTable dataTable = new DataTable();
            Connection CNObject = new Connection();
            CNObject.CreateConnection();
            CNObject.ConnectionOpen();
            const string sql = "Select customerName Customer, type Type,COUNT(*) Appointments from appointment join customer on appointment.customerId = customer.customerId GROUP BY Customer, Type";
            MySqlDataAdapter rdr = CNObject.SQLAdapter(sql);
            rdr.Fill(dataTable);
            rdr.Dispose();
            CNObject.ConnectionClose();
            AppointmentsGridView.DataSource = dataTable;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            //  Open Appointment form.
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
    }//  End of Class
}