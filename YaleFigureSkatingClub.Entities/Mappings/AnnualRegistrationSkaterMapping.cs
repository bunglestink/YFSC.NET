using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class AnnualRegistrationSkaterMapping : ClassMap<AnnualRegistrationSkater>
	{
		public AnnualRegistrationSkaterMapping ()
		{
			Table ("RegistrationSkaters");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.FirstName);
			Map (x => x.MiddleName);
			Map (x => x.LastName);
			Map (x => x.Sex);
			Map (x => x.USCitizen);
			Map (x => x.BirthDate);
			Map (x => x.NewRegistrant);
			Map (x => x.Level);
			
			HasManyToMany<SkatingSession>(x => x.Sessions)
				.Table ("SkatingSessionSkaters")
				.ParentKeyColumn("RegistrationSkaterID")
				.ChildKeyColumn("SkatingSessionID")
				.Inverse()
				.Cascade.All ();
		}
	}
}

