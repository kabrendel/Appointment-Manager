using System;

namespace Appointment_Manager
{
	public class City
	{
		public int CityId { get; set; }
		public string ACity { get; set; }
		public int CountryId { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public City(int _cityId, string _city, int _countryId, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			this.CityId = _cityId;
			this.ACity = _city;
			this.CountryId = _countryId;
			this.CreateDate = _createDate;
			this.CreatedBy = _createdBy;
			this.LastUpdate = _lastUpdate;
			this.LastUpdateBy = _lastUpdateBy;
		}
	}
}
