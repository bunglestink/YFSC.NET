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
	public class CoachController : Controller
	{
		readonly ISession session;
		
		public CoachController(ISession session) 
		{
			this.session = session;
		}
		
		public ActionResult Index()
		{
			Coach[] coach = session
				.Query<Coach>()
				.OrderBy(c => c.Name)
				.ToArray();
			
			return View(coach);
		}
		
		
		
		public ActionResult Create()
		{
			return View("Edit", new Coach());
		}
		
		
		public ActionResult Edit(int id)
		{
			Coach coach = 
				session
					.Query<Coach>()
					.SingleOrDefault(a => a.ID == id);
			
			if (coach == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (coach);
			}
		}
		

		[ValidateInput(false)]
		public ActionResult Commit(Coach coach)
		{
			session.SaveOrUpdate(coach);
			TempData["message"] = String.Format("Coach '{0}' saved.", coach.Name);
			return Redirect(Url.Action ("Index"));
		}
		
		
		public ActionResult DeleteConfirm(int id)
		{
			Coach coach = 
				session
					.Query<Coach>()
					.SingleOrDefault(a => a.ID == id);
			
			if (coach == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (coach);
			}
		}
		
		
		public ActionResult Delete(int id)
		{
			Coach coach = 
				session
					.Query<Coach>()
					.SingleOrDefault(a => a.ID == id);
			
			if (coach != null) {
				session.Delete(coach);
				TempData["message"] = String.Format("Coach '{0}' removed.", coach.Name);
			}
			
			return RedirectToAction("Index");
		}
	}
}

