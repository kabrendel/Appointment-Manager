using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public partial class Main : Form
    {
        //  Objects for Winforms.
        private Login LoginDialog;
        private Customers CustomerForm;
        private Appointments AppointmentForm;
        private Search SearchForm;
        //  Repository access object.
        private readonly Repository Repo;
        public Main()
        {
            InitializeComponent();
            Repo = new Repository();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            LoginDialog = new Login(this);
            var confirm = LoginDialog.ShowDialog();
            if (confirm == DialogResult.OK)
            {
                UserNotifications();
                UpdateAppointments(false);
            }
        }
        /// <summary>
        /// Checks appointments for logged in user and returns a message if an appointment is in the next 15 minutes.
        /// </summary>
        private void UserNotifications()
        {
            BindingList<Appointment> appointments = Repo.GetUserAppointments();
            foreach (Appointment a in appointments)
            {
                if ((a.Start > DateTime.UtcNow) && (a.Start <= DateTime.UtcNow.AddMinutes(15)))
                {
                    MessageBox.Show(Repo.GetUserName() + " has an appointment in the next " + a.Start.Subtract(DateTime.UtcNow).ToString("mm") + " minutes.", this.Text);
                    break;
                }
            }
        }
        /// <summary>
        /// Generates and data table of appointments for data grid view
        /// </summary>
        /// <param name="all">True for all users appointments or false for just the logged in user.</param>
        public void UpdateAppointments(bool all)
        {
            if (all)
            {
                AppointmentsGridView.DataSource = Repo.GetAppointmentTableAll();
            }
            else
            {
                AppointmentsGridView.DataSource = Repo.GetAppointmentTable();
            }
            AppointmentsGridView.Columns[0].Visible = false;
            AppointmentsGridView.Columns[2].Visible = false;
            AppointmentsGridView.Columns[4].Visible = false;
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
            if (CustomerForm?.IsDisposed != false)
            {
                CustomerForm = new Customers(this);
                CustomerForm.Show();
            }
            else
            {
                CustomerForm.BringToFront();
            }
        }
        private void ButtonApt_Click(object sender, EventArgs e)
        {
            //  Open Appointment form.
            if (AppointmentForm?.IsDisposed != false)
            {
                AppointmentForm = new Appointments(this);
                AppointmentForm.Show();
            }
            else
            {
                AppointmentForm.BringToFront();
            }
        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            //  Open Search form.
            if (SearchForm?.IsDisposed != false)
            {
                SearchForm = new Search(this);
                SearchForm.Show();
            }
            else
            {
                SearchForm.BringToFront();
            }
        }
        //  Buttons for "reports"
        private void ButtonMonthly_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = Repo.MonthlyReport();
        }
        private void ButtonConsultants_Click(object sender, EventArgs e)
        {
            UpdateAppointments(true);
            DataTable dataTable = (AppointmentsGridView.DataSource as DataTable);
            dataTable.DefaultView.RowFilter = null;
            dataTable.DefaultView.Sort = "User Name ASC,Start ASC";
        }
        private void ButtonCustomers_Click(object sender, EventArgs e)
        {
            AppointmentsGridView.DataSource = Repo.CustomerReport();
        }
        //  Buttons for logged in user "reports".
        private void ButtonAll_Click(object sender, EventArgs e)
        {
            //  Set DGV to all appointments for logged in user.
            UpdateAppointments(false);
        }
        private void ButtonWeek_Click(object sender, EventArgs e)
        {
            //  Set DGV to current weeks appointments.
            UpdateAppointments(false);
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
            DataView defaultView = (AppointmentsGridView.DataSource as DataTable)?.DefaultView;
            defaultView.RowFilter = string.Format("Start > '{0}' AND Start < '{1}'", start, end);
        }
        private void ButtonMonth_Click(object sender, EventArgs e)
        {
            //  Set DGV to current month.
            UpdateAppointments(false);
            DateTime start = DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day);
            DateTime end = DateTime.UtcNow.AddDays(DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month) - DateTime.UtcNow.Day);
            DataView defaultView = (AppointmentsGridView.DataSource as DataTable)?.DefaultView;
            defaultView.RowFilter = string.Format("Start > '{0}' AND Start < '{1}'", start, end);
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }//  End of Class
}