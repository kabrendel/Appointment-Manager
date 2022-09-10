using System;

namespace Appointment_Scheduler
{
	public class Country
	{
		public int CountryId { get; set; }
		public string ACountry { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Country(int countryId, string country, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			this.CountryId = countryId;
			this.ACountry = country;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.LastUpdate = lastUpdate;
			this.LastUpdateBy = lastUpdateBy;
		}
	}
}
