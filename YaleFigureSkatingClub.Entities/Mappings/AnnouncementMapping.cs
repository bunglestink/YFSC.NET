using System;
using System.Collections.Generic;
using System.Text;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class AnnouncementMapping : ClassMap<Announcement>
	{
		public AnnouncementMapping ()
		{
			Table ("Announcements");
			LazyLoad();
			Id (x => x.ID).GeneratedBy.Identity().Column ("ID");
			Map (x => x.AnnouncementDate);
			Map (x => x.Title).Length (255);
			Map (x => x.Body).Length(3000);
		}
	}
}

