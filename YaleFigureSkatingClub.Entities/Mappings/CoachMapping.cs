using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class CoachMapping : ClassMap<Coach>
	{
		public CoachMapping ()
		{
			Table ("Coaches");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.Name).Column("Name").Length (255);
			Map (x => x.PrimaryInfo).Column("PrimaryInfo").Length(3000);
			Map (x => x.SecondaryInfo).Column("SecondaryInfo").Length(3000);
		}
	}
}

