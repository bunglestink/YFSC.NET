using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub.Entities
{
    public class NHConfiguration
    {
        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
					PostgreSQLConfiguration.PostgreSQL82.ConnectionString(
						//"Server=devsql.kirkwthompson.com;Port=5432;Database=yfsc;User Id=postgres;Password=43kbeijK"))
						"Server=bunglestink.homeip.net;Port=5432;Database=yfsc;User Id=postgres;Password=43kbeijK"))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Announcement>())
                .BuildSessionFactory();
        }
    }
}
