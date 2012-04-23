using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using NHibernate;

namespace YaleFigureSkatingClub
{
	[Authorize]
	public class MembershipController : Controller
	{
		readonly ISession session;
		
		public MembershipController(ISession session) 
		{
			this.session = session;
		}
		
		public ActionResult Index()
		{
			YaleFigureSkatingClub.Entities.User user = session.Get<YaleFigureSkatingClub.Entities.User>(User.Identity.Name);
			return View (user);
		}
		
		public ActionResult Register()
		{
			return View ();
		}
	}
}

