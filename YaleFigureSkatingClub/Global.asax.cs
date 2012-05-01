using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using Ninject;
using YaleFigureSkatingClub.Infrastructure;
using YaleFigureSkatingClub.Infrastructure.Logging;
using YaleFigureSkatingClub.BusinessLayer;
using YaleFigureSkatingClub.Controllers;

namespace YaleFigureSkatingClub
{
	public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
	{
		static StandardKernel ninjectKernel;
		public IKernel NinjectKernel { get { return ninjectKernel; } }
		
		public static void RegisterRoutes (RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("favicon.ico");

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
			InitContainer();
			ControllerBuilder.Current.SetControllerFactory(typeof(NinjectControllerFactory));
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
		
		protected void Application_Error(object sender, EventArgs e)
		{
			var error = Server.GetLastError();
			var log = ninjectKernel.Get<ILog>();
			log.Error(error.Message);
			log.Error(error.StackTrace);
		}
		
		
		private static void InitContainer()
		{
			if (ninjectKernel == null) {
				ninjectKernel = new StandardKernel();
			}
			
			ninjectKernel.Bind<ISession>().ToMethod(x => NHSessionManager.CurrentSession);
			ninjectKernel.Bind<ILog>().To<SimpleLog>();
			ninjectKernel.Bind<IRegistrationService>().To<RegistrationService>();
		}
	}
}
