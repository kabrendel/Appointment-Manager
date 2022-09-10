using System;

namespace Appointment_Scheduler
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
		public User() { }
		public User(int userId, string userName, string password, byte active, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			this.UserId = userId;
			this.UserName = userName;
			this.Password = password;
			this.Active = active;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.LastUpdate = lastUpdate;
			this.LastUpdateBy = lastUpdateBy;
		}
	}
}
