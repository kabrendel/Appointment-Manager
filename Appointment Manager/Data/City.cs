using System;

namespace Appointment_Scheduler
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
		public City(int cityId, string city, int countryId, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			CityId = cityId;
			ACity = city;
			CountryId = countryId;
			CreateDate = createDate;
			CreatedBy = createdBy;
			LastUpdate = lastUpdate;
			LastUpdateBy = lastUpdateBy;
		}
	}
}
