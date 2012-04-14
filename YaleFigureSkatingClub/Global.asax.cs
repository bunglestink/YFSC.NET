using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using YaleFigureSkatingClub.Infrastructure;

namespace YaleFigureSkatingClub
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = "" }
            );

		}

		protected void Application_Start ()
		{
			RegisterRoutes(RouteTable.Routes);
			ModelBinders.Binders.DefaultBinder = new JsonModelBinder();
		}
		
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			NHSessionManager.CurrentSession.BeginTransaction();
		}
				
		/// <summary>
		/// Close NHSession after request for Open View in Session Pattern
		/// </summary>
		protected void Application_EndRequest(object sender, EventArgs e)
		{
			NHSessionManager.CloseSession();
		}
	}
}
