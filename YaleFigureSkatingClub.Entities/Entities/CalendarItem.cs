using System;

namespace YaleFigureSkatingClub.Entities
{
	public class CalendarItem
	{
		public virtual int ID { get; set; }
		public virtual string Days { get; set; }
		public virtual string SaturdayNotes { get; set; }
		public virtual string SundayNotes { get; set; }
		public virtual RegistrationTerm RegistrationTerm  { get; set; }
		
		public CalendarItem ()
		{
		}
	}
}

