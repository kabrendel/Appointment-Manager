using System;

namespace Appointment_Scheduler
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
		public Address(int addressId, string address, string address2, int cityId, string postalCode, string phone, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			AddressId = addressId;
			Address1 = address;
			Address2 = address2;
			CityId = cityId;
			PostalCode = postalCode;
			Phone = phone;
			CreateDate = createDate;
			CreatedBy = createdBy;
			LastUpdate = lastUpdate;
			LastUpdateBy = lastUpdateBy;
		}
	}
}
