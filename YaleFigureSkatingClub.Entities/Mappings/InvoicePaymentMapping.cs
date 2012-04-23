using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class InvoicePaymentMapping : ClassMap<InvoicePayment>
	{
		public InvoicePaymentMapping ()
		{
			Table("InvoicePayments");
			LazyLoad ();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.Description).Column("Description");
			Map (x => x.Type).Column ("Type");
			Map (x => x.Amount).Column ("Amount");
			Map (x => x.DateReceived).Column ("DateReceived");
			References(x => x.Invoice).Column("InvoiceID");
		}
	}
}

