using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class InvoiceMapping : ClassMap<Invoice>
	{
		public InvoiceMapping ()
		{
			Table("Invoices");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.InvoiceDate).Column ("InvoiceDate");
			References(x => x.Registration).Column ("RegistrationID");
			HasMany (x => x.InvoiceItems)
				.KeyColumn("InvoiceID")
				.Inverse()
				.Cascade.All ();
			HasMany (x => x.InvoicePayments)
				.KeyColumn("InvoiceID")
				.Inverse()
				.Cascade.All ();
		}
	}
}

