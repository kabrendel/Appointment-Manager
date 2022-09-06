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
		public User(int _userId, string _userName, string _password, byte _active, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			this.UserId = _userId;
			this.UserName = _userName;
			this.Password = _password;
			this.Active = _active;
			this.CreateDate = _createDate;
			this.CreatedBy = _createdBy;
			this.LastUpdate = _lastUpdate;
			this.LastUpdateBy = _lastUpdateBy;
		}
	}
}
