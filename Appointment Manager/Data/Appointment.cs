using System;

namespace Appointment_Scheduler
{
	public class Appointment
	{
		public int AppointmentId { get; set; }
		public int CustomerId { get; set; }
		public int UserId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public string Contact { get; set; }
		public string Type { get; set; }
		public string Url { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime LastUpdate { get; set; }
		public string LastUpdateBy { get; set; }
		public Appointment(int appointmentId, int customerId, int userId, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, DateTime createDate, string createdBy, DateTime lastUpdate, string lastUpdateBy)
		{
			this.AppointmentId = appointmentId;
			this.CustomerId = customerId;
			this.UserId = userId;
			this.Title = title;
			this.Description = description;
			this.Location = location;
			this.Contact = contact;
			this.Type = type;
			this.Url = url;
			this.Start = start;
			this.End = end;
			this.CreateDate = createDate;
			this.CreatedBy = createdBy;
			this.LastUpdate = lastUpdate;
			this.LastUpdateBy = lastUpdateBy;
		}
	}
}
