using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public partial class Appointments : Form
    {
        //  May not need.
        readonly Main main;
        //  Collections for dropboxes.
        private List<string> Type;
        private DataTable Customer;
        private DataTable User;
        //  Appointment Dialog form object.
        private AptmntD Dialog;
        //  Vars for adding, modifying, deleting appointments.
        public string aType { get; set; }
        public int aCustomer { get; set; }
        public int aUser { get; set; }
        public string aStart { get; set; }
        public string aStartTime { get; set; }
        public string aEnd { get; set; }
        public string aEndTime { get; set; }
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
            dataGridView1.DataSource = main.DTBuilder.BuildAppointmentTable();
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[User Id] = {0}", main.User.UserId);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void Appointments_Load(object sender, EventArgs e)
        {
            //  Get data from database
            Type = main.DTBuilder.TypeList();
            Customer = main.DTBuilder.CustomerList();
            User = main.DTBuilder.UserList(false);
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            Dialog = new AptmntD(Type, Customer, User, this);
            var confirmDelete = Dialog.ShowDialog();
            if (confirmDelete == DialogResult.OK)
            {
                // "accept"
                //  need to check appointment to make sure no overlap in schedule.
                DateTime startDate = DateTime.Parse(aStart) + TimeSpan.Parse(aStartTime);
                DateTime endDate = DateTime.Parse(aEnd) + TimeSpan.Parse(aEndTime);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (int.Parse(row.Cells["User Id"].Value.ToString()) == aUser)
                    {
                        //  startDate can't be between start or end of any existing appointment for the user.
                        if (startDate >= DateTime.Parse(row.Cells["Start"].Value.ToString()) && startDate < DateTime.Parse(row.Cells["End"].Value.ToString()))
                        {
                            MessageBox.Show("New appointment conflicts with existing appointment.", this.Text);
                            return;
                        }
                    }
                }
                if (main.CreateAppointment(aCustomer, aUser, aType, startDate, endDate))
                {
                    //  success
                    MessageBox.Show("Appointment created.", this.Text);
                }
                else
                {
                    //  Error message shown in method call.
                }
                //  reload appointments.
                main.DBObject.LoadAppointments(main.User.UserId);
                dataGridView1.DataSource = main.DTBuilder.BuildAppointmentTable();
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[User Id] = {0}", main.User.UserId);
            }
            else
            {
                //  exit
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Caller = "update";
            Dialog = new AptmntD(Type, Customer, User, this);
            var confirmDelete = Dialog.ShowDialog();
            if (confirmDelete == DialogResult.OK)
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                DateTime startDate = DateTime.Parse(aStart) + TimeSpan.Parse(aStartTime);
                DateTime endDate = DateTime.Parse(aEnd) + TimeSpan.Parse(aEndTime);
                //  Iterate and check for appointment conflict.
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    // Only check if not the appointment we're trying to update, can't conflict with self.
                    if (r.Cells["Appointment Id"].Value.ToString() != row.Cells["Appointment Id"].Value.ToString())
                    {
                        if (int.Parse(r.Cells["User Id"].Value.ToString()) == aUser)
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
                if (main.UpdateAppointment(int.Parse(row.Cells["Appointment Id"].Value.ToString()),aCustomer, aUser, aType, startDate, endDate))
                {
                    //  success
                    MessageBox.Show("Appointment updated.", this.Text);
                }
                else
                {
                    //  Error message shown in method call.
                }
                //  reload appointments.
                main.DBObject.LoadAppointments(main.User.UserId);
                dataGridView1.DataSource = main.DTBuilder.BuildAppointmentTable();
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[User Id] = {0}", main.User.UserId);
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
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                //  mainDeleteAppointment appointment id, customer id, userid
                if (main.RemoveAppointment(int.Parse(row.Cells["Appointment Id"].Value.ToString())))
                {
                    MessageBox.Show("Appointment deleted.", this.Text);
                    main.DBObject.LoadAppointments(main.User.UserId);
                    dataGridView1.DataSource = main.DTBuilder.BuildAppointmentTable();
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = String.Format("[User Id] = {0}", main.User.UserId);
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
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
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
