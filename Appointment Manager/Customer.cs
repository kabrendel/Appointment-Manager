using System;

namespace Appointment_Manager
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
		public int AddressId { get; set; }
		public bool Active { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Customer(int _customerId, string _customerName, int _addressId, bool _active, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			this.CustomerId = _customerId;
			this.CustomerName = _customerName;
			this.AddressId = _addressId;
			this.Active = _active;
			this.CreateDate = _createDate;
			this.CreatedBy = _createdBy;
			this.LastUpdate = _lastUpdate;
			this.LastUpdateBy = _lastUpdateBy;
		}
	}
}
