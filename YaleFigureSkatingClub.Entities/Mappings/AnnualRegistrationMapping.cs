using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class AnnualRegistrationMapping : ClassMap<AnnualRegistration>
	{
		public AnnualRegistrationMapping ()
		{
			Table ("Registrations");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.FirstName);
			Map (x => x.MiddleName);
			Map (x => x.LastName);
			Map (x => x.Street);
			Map (x => x.City);
			Map (x => x.State);
			Map (x => x.Zip);
			Map (x => x.HomePhone);
			Map (x => x.WorkPhone);
			Map (x => x.Email);
			Map (x => x.YaleAffiliation);
			Map (x => x.NameOfAffiliatedPerson);
			Map (x => x.YaleAffiliationType);
			Map (x => x.Department);
			Map (x => x.School);
			Map (x => x.Year);
			
			References(x => x.RegistrationTerm).Column("RegistrationTermID");
			References(x => x.User).Column("Username");
			HasOne(x => x.Invoice)
				.PropertyRef(x => x.Registration)
				.Cascade.All();
			HasMany (x => x.Skaters)
				.KeyColumn("RegistrationID")
				.Inverse()
				.Cascade.All ();
		}
	}
}

