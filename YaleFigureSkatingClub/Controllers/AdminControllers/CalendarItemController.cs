using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using YaleFigureSkatingClub.Models;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace YaleFigureSkatingClub
{
	[Authorize(Roles="Admin")]
	public class CalendarItemController : Controller
	{
		readonly ISession session;
		
		public CalendarItemController() 
		{
			this.session = NHSessionManager.CurrentSession;
		}
		
		//TODO: check if this works... idea using inherited ViewModel
		public ActionResult Index(int id)
		{
			RegistrationTerm term = session.Get<RegistrationTerm>(id);
			
			if (term == null) {
				return RedirectToAction("Index", "RegistrationTerm");
			}
            else {
				return View(term);
			}
		}
		
		
		
		public ActionResult Create(int id)
		{
			return View("Edit", new ViewCalendarItem(){ RegistrationTermID = id });
		}
		
		
		public ActionResult Edit(int id)
		{
			CalendarItem item = session.Get<CalendarItem>(id);
			ViewCalendarItem viewItem = new ViewCalendarItem(item);
			
			if (item == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (viewItem);
			}
		}
		
		
		[ValidateInput(false)]
		public ActionResult Commit(ViewCalendarItem item)
		{
			// get RegistrationTerm from RegistrationTermID
			RegistrationTerm term = session.Get<RegistrationTerm>(item.RegistrationTermID);
			item.RegistrationTerm = term;
			
			session.SaveOrUpdate(item.ToCalendarItem());
			return RedirectToAction("Index", new { id = item.RegistrationTerm.ID});
		}
		
		
		public ActionResult DeleteConfirm(int id)
		{
			CalendarItem item = session.Get<CalendarItem>(id);
			
			if (item == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (item);
			}
		}
		
		
		public ActionResult Delete(int id)
		{
			CalendarItem item = session.Get<CalendarItem>(id);
						
			if (item != null) {
				session.Delete(item);
			}
			
			return RedirectToAction("Index", new { id = item.RegistrationTerm.ID});
		}
	}
}

