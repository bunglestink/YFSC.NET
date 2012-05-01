using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NHibernate;
using NHibernate.Linq;

using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Models;

namespace YaleFigureSkatingClub.Controllers
{
	[Authorize(Roles="Admin")]
    public class SkatingSessionController : Controller
	{
		readonly ISession session;
		
        public SkatingSessionController(ISession session) 
		{
			this.session = session;
		}
		
		
		public ActionResult Index(int id)
		{
			var term = session.Get<RegistrationTerm>(id);
			
			if (term == null) {
				return RedirectToAction("Index", "RegistrationTerm");
			}
            else {
				return View(term);
			}
		}
		
		
		
		public ActionResult Create(int id)
		{
			return View("Edit", new ViewSkatingSession(){ RegistrationTermID = id });
		}
		
		
		public ActionResult Edit(int id)
		{
			var skatingSession = session.Get<SkatingSession>(id);
			
			
			if (skatingSession == null) {
				return RedirectToAction("Index");
			}
			
			var term = session.Query<RegistrationTerm>()
				.SingleOrDefault(t => t.SkatingSessions.Any(s => s.ID == skatingSession.ID));
			
			if (term == null) {
				return RedirectToAction("Index");
			}
					
			var viewSkatingSession = new ViewSkatingSession(skatingSession, term.ID);
			return View (viewSkatingSession);
			
		}
		
		
		[ValidateInput(false)]
		public ActionResult Commit(ViewSkatingSession skatingSession)
		{
			// get RegistrationTerm from RegistrationTermID
			var term = session.Get<RegistrationTerm>(skatingSession.RegistrationTermID);
			var realSession = skatingSession.ToSkatingSession();
			
			if (!term.SkatingSessions.Any(s => s.ID == realSession.ID)) {
				term.SkatingSessions.Add(realSession);
			}
			session.SaveOrUpdate(realSession);
			session.SaveOrUpdate(term);
			
			Console.WriteLine(
				new System.Web.Script.Serialization.JavaScriptSerializer().Serialize (term.SkatingSessions));
			
			TempData["message"] = String.Format("Skating session '{0}' saved.", skatingSession.ID);
			return RedirectToAction("Index", new { id = term.ID});
		}
		
		
		public ActionResult DeleteConfirm(int id)
		{
			var skatingSession = session.Get<SkatingSession>(id);
			var term = session.Query<RegistrationTerm>()
				.SingleOrDefault(t => t.SkatingSessions.Any (s => s.ID == id));
			
			if (skatingSession == null || term == null) {
				return RedirectToAction("Index", "RegistrationTerm");
			}
			else {
				return View (new ViewSkatingSession(skatingSession, term.ID));
			}
		}
		
		
		public ActionResult Delete(int id)
		{
			var skatingSession = session.Get<SkatingSession>(id);
						
			if (skatingSession == null) {
				return RedirectToAction("Index", "RegistrationTerm");
			}

			var term = session.Query<RegistrationTerm>()
				.SingleOrDefault(t => t.SkatingSessions.Any(s => s.ID == skatingSession.ID));
			if (term == null) {
				return RedirectToAction("Index", "RegistrationTerm");
			}
			
			term.SkatingSessions.Remove(skatingSession);
			session.Delete(skatingSession);
			TempData["message"] = String.Format("Skating session '{0}' removed.", skatingSession.ID);
			
			return RedirectToAction("Index", new { id = term.ID});
		}
    }
}
