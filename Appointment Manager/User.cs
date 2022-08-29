using System;

namespace Appointment_Manager
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public byte Active { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public User(int userId_, string userName_, string password_, byte active_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.UserId = userId_;
			this.UserName = userName_;
			this.Password = password_;
			this.Active = active_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
