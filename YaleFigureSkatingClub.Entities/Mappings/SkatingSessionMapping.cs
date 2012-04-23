using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class SkatingSessionMapping : ClassMap<SkatingSession>
	{
		public SkatingSessionMapping ()
		{
			Table ("SkatingSessions");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.Name);
			Map (x => x.StartDate);
			Map (x => x.WeeksDuration);
			Map (x => x.DayOfWeek);
			Map (x => x.StartTime);
			Map (x => x.EndTime);
			Map (x => x.TotalCost);
			Map (x => x.Description);
			
			References(x => x.RegistrationTerm).Column ("RegistrationTermID");
		}
	}
}

