using System;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.Models
{
	public class ViewSkatingSession : SkatingSession
	{
		public virtual int? RegistrationTermID { get; set; }
		
		public ViewSkatingSession () : base()
		{
			this.StartDate = DateTime.Today;
		}
		
		public ViewSkatingSession(SkatingSession skatingSession, int registrationTermID)
		{
			this.ID = skatingSession.ID;
			this.ID = skatingSession.ID;
			this.Name = skatingSession.Name;
			this.StartDate = skatingSession.StartDate;
			this.WeeksDuration = skatingSession.WeeksDuration;
			this.DayOfWeek = skatingSession.DayOfWeek;
			this.StartTime = skatingSession.StartTime;
			this.EndTime = skatingSession.EndTime;
			this.TotalCost = skatingSession.TotalCost;
			this.Description = skatingSession.Description;
			
			this.RegistrationTermID = registrationTermID;
		}
		
		
		public SkatingSession ToSkatingSession()
		{
			return new SkatingSession {
				ID = this.ID,
				Name = this.Name,
				StartDate = this.StartDate,
				WeeksDuration = this.WeeksDuration,
				DayOfWeek = this.DayOfWeek,
				StartTime = this.StartTime,
				EndTime = this.EndTime,
				TotalCost = this.TotalCost,
				Description = this.Description
			};
		}
	}
}