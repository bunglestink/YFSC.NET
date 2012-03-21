using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace YaleFigureSkatingClub
{
	[Authorize(Roles="Admin")]
	public class RegistrationTermController : Controller
	{
		readonly ISession session;
		
		public RegistrationTermController()
		{
			this.session = NHSessionManager.CurrentSession;
		}
		
		public ActionResult Index()
		{
			RegistrationTerm[] registrationTerms = 
				session
					.Query<RegistrationTerm>()
					.OrderByDescending(c => c.StartDate)
					.ToArray();
			
			return View(registrationTerms);
		}
		
		
		
		public ActionResult Create()
		{
			return View("Edit", new RegistrationTerm(){StartDate = DateTime.Today, EndDate = DateTime.Today});
		}
		
		
		public ActionResult Edit(int id)
		{
			RegistrationTerm term = 
				session
					.Query<RegistrationTerm>()
					.SingleOrDefault(a => a.ID == id);
			
			if (term == null)
				return RedirectToAction("Index");
			else
				return View (term);
		}
		
		
		public ActionResult Commit(RegistrationTerm term)
		{
			session.SaveOrUpdate(term);
			return Redirect(Url.Action ("Index"));
		}
		
		
		public ActionResult DeleteConfirm(int id)
		{
			RegistrationTerm term = 
				session
					.Query<RegistrationTerm>()
					.SingleOrDefault(a => a.ID == id);
			
			if (term == null) {
				return RedirectToAction("Index");
			}
			if (term.CalendarItems.Count > 0) {
				return View ("DeleteError");
			}
			
			return View (term);
		}
		
		
		public ActionResult Delete(int id)
		{
			RegistrationTerm term = 
				session
					.Query<RegistrationTerm>()
					.SingleOrDefault(a => a.ID == id);
			
			if (term.CalendarItems.Count > 0) {
				// do not delete if has calendar items
				return View ("DeleteError");
			}

			if (term != null) {
				session.Delete(term);
			}
				
			return RedirectToAction("Index");
		}
	}
}

