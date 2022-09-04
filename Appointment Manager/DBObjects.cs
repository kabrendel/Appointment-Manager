using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_Manager
{
    internal class DBObjects
    {
        //  Collections for database objects.
        public BindingList<Address> Addresses;
        public BindingList<Appointment> Appointments;
        public BindingList<City> Cities;
        public BindingList<Country> Countries;
        public BindingList<Customer> Customers;
        public BindingList<User> Users;
        //

    }//  End of Class
}
