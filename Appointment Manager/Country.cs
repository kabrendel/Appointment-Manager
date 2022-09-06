using System;

namespace Appointment_Manager
{
	public class Country
	{
		public int CountryId { get; set; }
		public string ACountry { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Country(int _countryId, string _country, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			this.CountryId = _countryId;
			this.ACountry = _country;
			this.CreateDate = _createDate;
			this.CreatedBy = _createdBy;
			this.LastUpdate = _lastUpdate;
			this.LastUpdateBy = _lastUpdateBy;
		}
	}
}
