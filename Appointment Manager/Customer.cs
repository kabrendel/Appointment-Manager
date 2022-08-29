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
		public Customer(int customerId_, string customerName_, int addressId_, bool active_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.CustomerId = customerId_;
			this.CustomerName = customerName_;
			this.AddressId = addressId_;
			this.Active = active_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
