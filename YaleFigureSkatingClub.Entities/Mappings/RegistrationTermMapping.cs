using System;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class RegistrationTermMapping : ClassMap<RegistrationTerm>
	{
		public RegistrationTermMapping ()
		{
			Table ("RegistrationTerms");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column("ID");
			Map (x => x.TermName).Column("TermName").Length(255);
			Map (x => x.StartDate).Column("StartDate");
			Map (x => x.EndDate).Column("EndDate");
			Map (x => x.Current).Column("Current");
			//HasMany (x => x.AnnualRegistrations).KeyColumn("RegistrationTermID");
			HasMany(x => x.SkatingSessions)
				.KeyColumn("RegistrationTermID")
				.Inverse()
				.Cascade.All();
			HasMany (x => x.CalendarItems)
				.KeyColumn("RegistrationTermID")
				.Inverse()
				.Cascade.All ();
		}
	}
}

