using System;

namespace Appointment_Manager
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
		public Appointment(int _appointmentId, int _customerId, int _userId, string _title, string _description, string _location, string _contact, string _type, string _url, DateTime _start, DateTime _end, DateTime _createDate, string _createdBy, DateTime _lastUpdate, string _lastUpdateBy)
		{
			this.AppointmentId = _appointmentId;
			this.CustomerId = _customerId;
			this.UserId = _userId;
			this.Title = _title;
			this.Description = _description;
			this.Location = _location;
			this.Contact = _contact;
			this.Type = _type;
			this.Url = _url;
			this.Start = _start;
			this.End = _end;
			this.CreateDate = _createDate;
			this.CreatedBy = _createdBy;
			this.LastUpdate = _lastUpdate;
			this.LastUpdateBy = _lastUpdateBy;
		}
	}
}
