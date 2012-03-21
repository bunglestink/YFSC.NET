using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace YaleFigureSkatingClub
{
	[Authorize]
	public class MembershipController : Controller
	{
		public ActionResult Index()
		{
			return View ();
		}
		
		public ActionResult Register()
		{
			return View ();
		}
	}
}

