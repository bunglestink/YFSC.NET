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
			var user = session.Get<YaleFigureSkatingClub.Entities.User>(User.Identity.Name);
			return View (user);
		}
		
		public ActionResult Invoice(int id, string format, string status)
		{
			var invoice = session.Get<YaleFigureSkatingClub.Entities.Invoice>(id);
			if (invoice == null) {
				return RedirectToAction("Index");
			}
			
			if (status != null && status == "success") {
				TempData["message"] = "Your registration has been successfully submited!  Please print this invoice and submit with payment.";
			}
			if (format != null && format == "csv") {
				return View("CsvInvoice", invoice);
			}
			return View("WebInvoice", invoice);
		}
		
		
		public ActionResult Register()
		{
			return View ();
		}
	}
}

