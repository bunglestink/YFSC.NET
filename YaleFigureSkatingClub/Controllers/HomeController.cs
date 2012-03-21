using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		readonly ISession session;
		
		public HomeController()
		{
			session = NHSessionManager.CurrentSession;
		}
		
		
		public ActionResult Index()
		{
			Announcement[] announcements = 
				session
					.Query<Announcement>()
					.OrderByDescending(a => a.AnnouncementDate)
					.Take (5)
					.ToArray();
			
			return View(announcements);
		}
		
		
		public ActionResult Program()
		{
			return View();
		}
		
		
		
		public ActionResult Calendar()
		{
			RegistrationTerm term = session.Query<RegistrationTerm>().Take (1).SingleOrDefault();
			
			return View(term);
		}
		
		
		public ActionResult ClubCoaches(int? id)
		{
			// if id is null, display all coaches
			if (id == null)
			{
				Coach[]  coaches = session.Query<Coach>().ToArray();
				return View("Coaches", coaches);
			}
			
			
			// if id, get individual coach, ensure exists or redirect home
			Coach coach = session.Get<Coach>(id);
			if (coach == null)
			{
				return Redirect(Url.Action("Index"));
			}
			return View ("Coach", coach);
		}
		
		
		public ActionResult Membership()
		{
			return View ();
		}
		
		public ActionResult Brochure()
		{
			return View ();
		}
		
		
		public ActionResult ByLaws()
		{
			return View();
		}
		
		
		
		public ActionResult ContactInformation()
		{
			return View();
		}
	}
}

