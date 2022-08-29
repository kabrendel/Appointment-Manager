using System;

namespace Appointment_Manager
{
	public class City
	{
		public int CityId { get; set; }
		public string city { get; set; }
		public int CountryId { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public City(int cityId_, string city_, int countryId_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.CityId = cityId_;
			this.city = city_;
			this.CountryId = countryId_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
