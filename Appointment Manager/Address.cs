using System;

namespace Appointment_Manager
{
	public class Address
	{
		public int AddressId { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public int CityId { get; set; }
		public string PostalCode { get; set; }
		public string Phone { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Address(int addressId_, string address_, string address2_, int cityId_, string postalCode_, string phone_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.AddressId = addressId_;
			this.Address1 = address_;
			this.Address2 = address2_;
			this.CityId = cityId_;
			this.PostalCode = postalCode_;
			this.Phone = phone_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
