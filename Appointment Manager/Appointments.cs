using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public partial class Appointments : Form
    {
        readonly Main main;
        //  Collections for dropboxes.
        private List<string> Type;
        private DataTable Customer;
        private DataTable User;
        //  Appointment Dialog form object.
        private AppointmentDialog Dialog;
        //  Vars for adding, modifying, deleting appointments.
        public string AType { get; set; }
        public int ACustomer { get; set; }
        public int AUser { get; set; }
        public string AStart { get; set; }
        public string AStartTime { get; set; }
        public string AEnd { get; set; }
        public string AEndTime { get; set; }
        public string Caller { get; set; }
        public Appointments(Main main)
        {
            InitializeComponent();
            this.main = main;
            Location = main.Location;

            Type = new List<string>();
            Customer = new DataTable();
            User = new DataTable();
            main.DBObject.LoadAppointments(main.User.UserId);
            AppointmentGridView.DataSource = main.DTBuilder.BuildAppointmentTable();
            AppointmentGridView.Columns[0].Visible = false;
            AppointmentGridView.Columns[2].Visible = false;
            AppointmentGridView.Columns[4].Visible = false;
        }

        private void Appointments_Load(object sender, EventArgs e)
        {
            Type = main.DTBuilder.TypeList();
            Customer = main.DTBuilder.CustomerList();
            User = main.DTBuilder.UserList(false);
        }

        private void AppointmentGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            AppointmentGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            main.DBObject.LoadAppointments(main.User.UserId);
            main.UpdateAppointments();
            Dispose();
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            Caller = "create";
            Dialog = new AppointmentDialog(Type, Customer, User, this);
            var confirmDelete = Dialog.ShowDialog();
            if (confirmDelete == DialogResult.OK)
            {
                //  need to check appointment to make sure no overlap in schedule.
                DateTime startDate = DateTime.Parse(AStart) + TimeSpan.Parse(AStartTime);
                DateTime endDate = DateTime.Parse(AEnd) + TimeSpan.Parse(AEndTime);
                foreach (DataGridViewRow row in AppointmentGridView.Rows)
                {
                    if (int.Parse(row.Cells["User Id"].Value.ToString()) == AUser)
                    {
                        //  startDate can't be between start or end of any existing appointment for the user.
                        if (startDate >= DateTime.Parse(row.Cells["Start"].Value.ToString()) && startDate < DateTime.Parse(row.Cells["End"].Value.ToString()))
                        {
                            MessageBox.Show("New appointment conflicts with existing appointment.", this.Text);
                            return;
                        }
                    }
                }
                if (main.SQLFunctions.CreateAppointment(ACustomer, AUser, AType, startDate, endDate))
                {
                    MessageBox.Show("Appointment created.", this.Text);
                }
                else
                {
                    //  Error message shown in method call.
                }
                //  reload appointments.
                main.DBObject.LoadAppointments(main.User.UserId);
                AppointmentGridView.DataSource = main.DTBuilder.BuildAppointmentTable();
            }
            else
            {
                //  exit
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Caller = "update";
            Dialog = new AppointmentDialog(Type, Customer, User, this);
            var confirmDelete = Dialog.ShowDialog();
            if (confirmDelete == DialogResult.OK)
            {
                DataGridViewRow row = AppointmentGridView.Rows[AppointmentGridView.CurrentCell.RowIndex];
                DateTime startDate = DateTime.Parse(AStart) + TimeSpan.Parse(AStartTime);
                DateTime endDate = DateTime.Parse(AEnd) + TimeSpan.Parse(AEndTime);
                //  Iterate and check for appointment conflict.
                foreach (DataGridViewRow r in AppointmentGridView.Rows)
                {
                    // Only check if not the appointment we're trying to update, can't conflict with self.
                    if (r.Cells["Appointment Id"].Value.ToString() != row.Cells["Appointment Id"].Value.ToString())
                    {
                        if (int.Parse(r.Cells["User Id"].Value.ToString()) == AUser)
                        {
                            //  startDate can't be between start or end of any existing appointment for the user.
                            if (startDate >= DateTime.Parse(r.Cells["Start"].Value.ToString()) && startDate <= DateTime.Parse(r.Cells["End"].Value.ToString()))
                            {
                                MessageBox.Show("Updated appointment conflicts with an existing appointment.", this.Text);
                                return;
                            }
                        }
                    }
                }
                if (main.SQLFunctions.UpdateAppointment(int.Parse(row.Cells["Appointment Id"].Value.ToString()),ACustomer, AUser, AType, startDate, endDate))
                {
                    MessageBox.Show("Appointment updated.", this.Text);
                }
                else
                {
                    //  Error message shown in method call.
                }
                //  reload appointments.
                main.DBObject.LoadAppointments(main.User.UserId);
                AppointmentGridView.DataSource = main.DTBuilder.BuildAppointmentTable();
            }
            else
            {
                //  exit
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var confirmDelete = MessageBox.Show("Are you sure you want to delete this appointment?", this.Text, MessageBoxButtons.OKCancel);
            if (confirmDelete == DialogResult.OK)
            {
                DataGridViewRow row = AppointmentGridView.Rows[AppointmentGridView.CurrentCell.RowIndex];
                if (main.SQLFunctions.RemoveAppointment(int.Parse(row.Cells["Appointment Id"].Value.ToString())))
                {
                    MessageBox.Show("Appointment deleted.", this.Text);
                    main.DBObject.LoadAppointments(main.User.UserId);
                    AppointmentGridView.DataSource = main.DTBuilder.BuildAppointmentTable();
                }
                else
                {
                    //  Error message shown in method call.
                }
            }
            else
            {
                return;
            }
        }

        public string[] GetSelected()
        {
            DataGridViewRow row = AppointmentGridView.Rows[AppointmentGridView.CurrentCell.RowIndex];
            DateTime start = DateTime.Parse(row.Cells["Start"].Value.ToString());
            DateTime end = DateTime.Parse(row.Cells["End"].Value.ToString());
            string[] selected = new string[]
            {
                row.Cells["User Name"].Value.ToString(),
                row.Cells["Customer Name"].Value.ToString(),
                row.Cells["Type"].Value.ToString(),
                start.Date.ToString(),
                start.ToString("h:mm tt"),
                end.ToString("h:mm tt")
            };
            return selected;
        }
    }
}
