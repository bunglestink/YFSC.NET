using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class CalendarItemMapping : ClassMap<CalendarItem>
	{
		public CalendarItemMapping ()
		{
			Table ("CalendarItems");
			LazyLoad();
			Id (x => x.ID);
			Map (x => x.Days).Column ("Days").Length(255);
			Map (x => x.SaturdayNotes).Column ("SaturdayNotes").Length(3000);
			Map (x => x.SundayNotes).Column ("SundayNotes").Length(3000);
			References(x => x.RegistrationTerm).Column ("RegistrationTermID");
		}
	}
}