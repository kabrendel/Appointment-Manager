using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Appointment_Manager
{
    public partial class Customers : Form
    {
        readonly Main main;
        List<TextBox> TextBoxes;
        public Customers(Main main)
        {
            InitializeComponent();
            //  Collection for data check.
            TextBoxes = new List<TextBox>();
            TextBoxes.Add(textName);
            TextBoxes.Add(textAdd1);
            //TextBoxes.Add(textAdd2);  //  Left off, Add2 is generally optional.
            TextBoxes.Add(textCity);
            TextBoxes.Add(textPostal);
            TextBoxes.Add(textCountry);
            TextBoxes.Add(textPhone);
            this.main = main;
            Location = main.Location;
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            main.UpdateAppointments();
            Dispose();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = main.BuildCustomerTable();            
            //  Hide Id columns.
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateText())
            {
                MessageBox.Show("Error with customer fields, double check entries.",this.Text);
                return;
            }
            if (main.AddCustomer(
                int.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString()),
                textName.Text,
                textAdd1.Text,
                textAdd2.Text,
                textCity.Text,
                textPostal.Text,
                textCountry.Text,
                textPhone.Text
                ))
            {
                //  true
                MessageBox.Show("Customer add success.", this.Text);
                //  Refresh DataSource.
                dataGridView1.DataSource = main.BuildCustomerTable();
            }
            else
            {
                //  false
                MessageBox.Show("Customer add failed. Please make sure you have selected the 'New Customer' row.", this.Text);
            }
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            if (row.Cells["Customer Name"].Value.ToString() == "New Customer")
            {
                //  No cell selected or column header selected.
                MessageBox.Show("Selected row is not updateable.", this.Text);
                return;
            }
            if (!ValidateText())
            {
                MessageBox.Show("Error with customer fields, double check entries.", this.Text);
                return;
            }
            if (main.UpdateCustomer(
                int.Parse(row.Cells["Customer Id"].Value.ToString()),
                int.Parse(row.Cells["Address Id"].Value.ToString()),
                int.Parse(row.Cells["City Id"].Value.ToString()),
                int.Parse(row.Cells["Country Id"].Value.ToString()),
                textName.Text,
                textAdd1.Text,
                textAdd2.Text,
                textCity.Text,
                textPostal.Text,
                textCountry.Text,
                textPhone.Text
                ))
            {
                //  true
                MessageBox.Show("Customer updated successfully.", this.Text);
                //  Refresh DataSource.
                dataGridView1.DataSource = main.BuildCustomerTable();
            }
            else
            {
                //  false
                MessageBox.Show("Customer update failed.", this.Text);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            var confirmDelete = MessageBox.Show("Are you sure you want to delete this customer?", this.Text, MessageBoxButtons.OKCancel);
            if (confirmDelete == DialogResult.OK)
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                if (row.Cells["Customer Name"].Value.ToString() == "New Customer")
                {
                    //  No cell selected or column header selected.
                    MessageBox.Show("Selected row is not deletable.", this.Text);
                    return;
                }
                else
                {
                    //  Update text boxes to selected data row.
                    int cust = int.Parse(row.Cells["Customer Id"].Value.ToString());
                    int addr = int.Parse(row.Cells["Address Id"].Value.ToString());
                    int city = int.Parse(row.Cells["City Id"].Value.ToString());
                    int cntry = int.Parse(row.Cells["Country Id"].Value.ToString());
                    if (main.DeleteCustomer(cust, addr, city, cntry))
                    {
                        MessageBox.Show("Customer successfully deleted.", this.Text);
                        dataGridView1.DataSource = main.BuildCustomerTable();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting customer.", this.Text);
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if ((dataGridView1.CurrentCell == null) || (dataGridView1.CurrentCell.RowIndex <= -1))
            {
                //  No cell selected or column header selected.
                return;
            }
            else
            {
                //  Update text boxes to selected data row.
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                textName.Text = row.Cells["Customer Name"].Value.ToString();
                textAdd1.Text = row.Cells["Address1"].Value.ToString();
                textAdd2.Text = row.Cells["Address2"].Value.ToString();
                textCity.Text = row.Cells["City"].Value.ToString();
                textPostal.Text = row.Cells["Postal Code"].Value.ToString();
                textCountry.Text = row.Cells["Country"].Value.ToString();
                textPhone.Text = row.Cells["Phone number"].Value.ToString();
            }
        }
        
        private bool ValidateText()
        {
            bool valid = true;
            foreach (TextBox txt in TextBoxes)
            {
                if ((string.IsNullOrEmpty(txt.Text)) || txt.Text.Length == 0)
                {
                    txt.BackColor = Color.IndianRed;
                    valid = false;
                }
                else
                {
                    txt.BackColor = Color.White;
                }
                if ((txt.Name == "textName") && (txt.Text == "New Customer"))
                {
                    txt.BackColor = Color.IndianRed;
                    valid = false;
                }
            }
            return valid;
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}