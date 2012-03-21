using System;
using System.Collections.Generic;

namespace YaleFigureSkatingClub.Entities
{
	public class RegistrationTerm
	{
		public virtual int ID { get; set; }
		public virtual string TermName { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		
		public virtual IList<AnnualRegistration> AnnualRegistrations { get; set; }
		public virtual IList<SkatingSession> SkatingSessions { get; set; }
		public virtual IList<CalendarItem> CalendarItems { get; set; }
		
		
		public RegistrationTerm ()
		{
			AnnualRegistrations = new List<AnnualRegistration>();
			SkatingSessions = new List<SkatingSession>();
			CalendarItems = new List<CalendarItem>();
		}
	}
}

