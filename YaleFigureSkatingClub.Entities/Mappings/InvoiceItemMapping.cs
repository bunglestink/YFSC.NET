using System;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class InvoiceItemMapping : ClassMap<InvoiceItem>
	{
		public InvoiceItemMapping ()
		{
			Table("InvoiceItems");
			LazyLoad ();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.Description).Column("Description");
			Map (x => x.UnitCost).Column ("UnitCost");
			Map (x => x.Quantity).Column ("Quantity");
			References(x => x.Invoice).Column("InvoiceID");
		}
	}
}

