using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace YaleFigureSkatingClub.Entities
{
	public class RoleMapping : ClassMap<Role>
	{
		public RoleMapping ()
		{
			Table ("Roles");
			LazyLoad();
			Id (x => x.ID);
			Map (x => x.Username).Column ("Username").Length(255);
			Map (x => x.Rolename).Column ("Role").Length(255);
			References(x => x.User).Column ("Username");
		}
	}
}