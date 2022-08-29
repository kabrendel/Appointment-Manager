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
		public Appointment(int appointmentId_, int customerId_, int userId_, string title_, string description_, string location_, string contact_, string type_, string url_, DateTime start_, DateTime end_, DateTime createDate_, string createdBy_, DateTime lastUpdate_, string lastUpdateBy_)
		{
			this.AppointmentId = appointmentId_;
			this.CustomerId = customerId_;
			this.UserId = userId_;
			this.Title = title_;
			this.Description = description_;
			this.Location = location_;
			this.Contact = contact_;
			this.Type = type_;
			this.Url = url_;
			this.Start = start_;
			this.End = end_;
			this.CreateDate = createDate_;
			this.CreatedBy = createdBy_;
			this.LastUpdate = lastUpdate_;
			this.LastUpdateBy = lastUpdateBy_;
		}
	}
}
