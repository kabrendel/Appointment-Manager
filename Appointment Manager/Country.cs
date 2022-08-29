using System;

namespace Appointment_Manager
{
	public class Country
	{
		public int CountryId { get; set; }
		public string country { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Country(int countryId_, string country_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.CountryId = countryId_;
			this.country = country_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
