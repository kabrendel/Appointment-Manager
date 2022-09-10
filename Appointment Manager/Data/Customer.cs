using System;

namespace Appointment_Scheduler
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
        public Customer() { }
        public Customer(int customerId, string customerName, int addressId, bool active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			this.CustomerId = customerId;
			this.CustomerName = customerName;
			this.AddressId = addressId;
			this.Active = active;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.LastUpdate = lastUpdate;
			this.LastUpdateBy = lastUpdateBy;
		}
	}
}
