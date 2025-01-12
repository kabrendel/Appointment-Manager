﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Appointment_Scheduler
{
    public partial class Search : Form
    {
        //
        private readonly Main main;
        private readonly Repository repo;
        //
        private List<string> Type;
        private DataTable Customer;
        private DataTable User;
        public Search(Main main)
        {
            InitializeComponent();
            this.main = main;
            Location = main.Location;
            repo = new Repository();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            //  Loast list of types from data.
            Type = repo.GetTypeList();
            cmbType.DataSource = Type;
            cmbType.SelectedIndex = -1;
            //  Load list of customer's from data.
            Customer = repo.GetCustomerList();
            cmbCust.DisplayMember = "Name";
            cmbCust.ValueMember = "ID";
            cmbCust.DataSource = Customer;
            cmbCust.SelectedIndex = -1;
            //  Load user list from data.
            User = repo.GetUserList(true);
            cmbUser.DisplayMember = "Name";
            cmbUser.ValueMember = "ID";
            cmbUser.DataSource = User;
            cmbUser.SelectedIndex = -1;
            //
            cmbStartTime.DisplayMember = "Time";
            cmbStartTime.ValueMember = "Span";
            cmbStartTime.DataSource = BuildComboTime();
            cmbStartTime.SelectedIndex = 0;
            //
            cmbEndTime.DisplayMember = "Time";
            cmbEndTime.ValueMember = "Span";
            cmbEndTime.DataSource = BuildComboTime();
            cmbEndTime.SelectedIndex = cmbEndTime.Items.Count - 1;
            //
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MaxDate;
            SearchGridView.DataSource = repo.GetAppointmentTableAll();
            SearchGridView.Columns[0].Visible = false;
            SearchGridView.Columns[2].Visible = false;
            SearchGridView.Columns[4].Visible = false;
        }
        //  Events
        private void CmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((!(SearchGridView.DataSource is null)) && (!(cmbUser.SelectedItem is null)))
            {
                SetDataFilter();
            }
        }
        private void CmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((!(SearchGridView.DataSource is null)) && (!(cmbCust.SelectedItem is null)))
            {
                SetDataFilter();
            }
        }
        private void SearchGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SearchGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((!(SearchGridView.DataSource is null)) && (!(cmbType.SelectedItem is null)))
            {
                SetDataFilter();
            }
        }
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!(SearchGridView.DataSource is null))
            {
                SetDataFilter();
            }
        }
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (!(SearchGridView.DataSource is null))
            {
                SetDataFilter();
            }
        }
        private void CmbStartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(SearchGridView.DataSource is null))
            {
                SetDataFilter();
            }
        }
        private void CmbEndTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(SearchGridView.DataSource is null))
            {
                SetDataFilter();
            }
        }
        //  Methods
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
        private void SetDataFilter()
        {
            List<string> filters = new List<string>();
            if (cmbUser.SelectedIndex != -1) { filters.Add(String.Format("[User ID] = {0}", cmbUser.SelectedValue)); }
            if (cmbCust.SelectedIndex != -1) { filters.Add(String.Format("[Customer ID] = {0}", cmbCust.SelectedValue)); }
            if (cmbType.SelectedIndex != -1) { filters.Add(String.Format("[Type] = '{0}'", cmbType.SelectedValue)); }

            DateTime startDate = dateTimePicker1.Value.Date + TimeSpan.Parse(cmbStartTime.SelectedValue.ToString());
            DateTime endDate = dateTimePicker2.Value.Date + TimeSpan.Parse(cmbEndTime.SelectedValue.ToString());

            filters.Add(String.Format("[Start] >= #{0}# AND [END] <= #{1}#", startDate, endDate));

            StringBuilder finalfilter = new StringBuilder();
            foreach (string s in filters)
            {
                if (finalfilter.Length > 0)
                {
                    finalfilter.Append(" AND ");
                }
                finalfilter.Append(s);
            }
            DataTable dataTable = SearchGridView.DataSource as DataTable;
            dataTable.DefaultView.RowFilter = finalfilter.ToString();
        }
        //  Buttons
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            cmbUser.SelectedIndex = -1;
            cmbCust.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker1.MaxDate;
            cmbStartTime.SelectedIndex = 0;
            cmbEndTime.SelectedIndex = cmbEndTime.Items.Count - 1;
            DataTable dataTable = SearchGridView.DataSource as DataTable;
            dataTable.DefaultView.RowFilter = string.Empty;
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }// End of class.
}