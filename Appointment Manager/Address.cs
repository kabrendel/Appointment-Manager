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
		public Address(int _addressId, string _address, string _address2, int _cityId, string _postalCode, string _phone, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			AddressId = _addressId;
			Address1 = _address;
			Address2 = _address2;
			CityId = _cityId;
			PostalCode = _postalCode;
			Phone = _phone;
			CreateDate = _createDate;
			CreatedBy = _createdBy;
			LastUpdate = _lastUpdate;
			LastUpdateBy = _lastUpdateBy;
		}
	}
}
