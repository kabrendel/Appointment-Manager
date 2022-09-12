using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public partial class AppointmentDialog : Form
    {
        //  Winform objects.
        readonly Appointments apt;
        public AppointmentDialog(List<string> Type, DataTable Customer, DataTable User, Appointments apt)
        {
            InitializeComponent();
            this.apt = apt;
            apt.AType = null;
            apt.ACustomer = 0;
            apt.AUser = 0;
            apt.AStart = null;
            apt.AStartTime = null;
            apt.AEnd = null;
            apt.AEndTime = null;
            //
            cmbType.DataSource = Type;
            //
            cmbCust.DisplayMember = "Name";
            cmbCust.ValueMember = "ID";
            cmbCust.DataSource = Customer;
            //
            cmbUser.DisplayMember = "Name";
            cmbUser.ValueMember = "ID";
            cmbUser.DataSource = User;
            //
            cmbStartTime.DisplayMember = "Time";
            cmbStartTime.ValueMember = "Span";
            cmbStartTime.DataSource = BuildComboTime();
            //
            cmbEndTime.DisplayMember = "Time";
            cmbEndTime.ValueMember = "Span";
            cmbEndTime.DataSource = BuildComboTime();
            //
            //  Set values for updating an appointment
            SetSelected();
        }
        private DataTable BuildComboTime()
        {
            TimeSpan open = new TimeSpan(08, 00, 00);
            TimeSpan close = new TimeSpan(20, 00, 00);
            TimeSpan increment = new TimeSpan(00, 15, 00);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Time", typeof(string));
            dataTable.Columns.Add("Span", typeof(string));
            while (open <= close)
            {
                DataRow row = dataTable.NewRow();
                row["Time"] = DateTime.Parse(open.ToString()).ToString("t");
                row["Span"] = open.ToString();
                dataTable.Rows.Add(row);
                open = open.Add(increment);
            }
            return dataTable;
        }
        private void CmbStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbEndTime.DataSource != null) && (cmbStartTime.DataSource != null))
            {
                buttonAccept.Enabled = DataCheck();
            }
        }
        private void CmbEndTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbEndTime.DataSource != null) && (cmbStartTime.DataSource != null))
            {
                buttonAccept.Enabled = DataCheck();
            }
        }
        private bool DataCheck()
        {
            if (cmbStartTime.SelectedIndex == 0 && cmbEndTime.SelectedIndex == 0)
            {
                lblStart.ForeColor = System.Drawing.Color.Red;
                lblEnd.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            if (cmbStartTime.SelectedIndex == cmbEndTime.SelectedIndex)
            {
                lblStart.ForeColor = System.Drawing.Color.Red;
                lblEnd.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            else if ((cmbEndTime.SelectedIndex >= 0) && (cmbEndTime.SelectedIndex <= (cmbEndTime.DataSource as DataTable).Rows.Count - 1))
            {
                if (cmbEndTime.SelectedIndex > cmbStartTime.SelectedIndex)
                {
                    lblStart.ForeColor = System.Drawing.Color.Black;
                    lblEnd.ForeColor = System.Drawing.Color.Black;
                    return true;
                }
                else
                {
                    lblEnd.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            else if ((cmbStartTime.SelectedIndex >= 0) && (cmbStartTime.SelectedIndex <= (cmbStartTime.DataSource as DataTable).Rows.Count - 1))
            {
                if (cmbStartTime.SelectedIndex < cmbEndTime.SelectedIndex)
                {
                    lblStart.ForeColor = System.Drawing.Color.Black;
                    lblEnd.ForeColor = System.Drawing.Color.Black;
                    return true;
                }
                else
                {
                    lblStart.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            else
            {
                lblStart.ForeColor = System.Drawing.Color.Red;
                lblEnd.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        private void SetSelected()
        {
            //  Set form values to values from selected appointment to update.
            if (apt.Caller == "update")
            {
                string[] selected = apt.GetSelected();
                cmbUser.SelectedIndex = cmbUser.FindString(selected[0]);
                cmbCust.SelectedIndex = cmbCust.FindString(selected[1]);
                cmbType.SelectedIndex = cmbType.FindString(selected[2]);
                dtpStart.Value = DateTime.Parse(selected[3]);
                cmbStartTime.SelectedIndex = cmbStartTime.FindString(selected[4]);
                cmbEndTime.SelectedIndex = cmbEndTime.FindString(selected[5]);
            }
        }
        //  Buttons
        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            apt.AType = cmbType.SelectedItem.ToString();                    //  Appointment Type
            apt.ACustomer = int.Parse(cmbCust.SelectedValue.ToString());    //  Customer Id
            apt.AUser = int.Parse(cmbUser.SelectedValue.ToString());        //  User Id
            apt.AStart = dtpStart.Value.Date.ToString();
            apt.AStartTime = cmbStartTime.SelectedValue.ToString();
            apt.AEnd = dtpStart.Value.Date.ToString();
            apt.AEndTime = cmbEndTime.SelectedValue.ToString();
        }
        //  End of class.
    }
}
