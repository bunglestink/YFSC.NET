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
	public class AnnouncementController : Controller
	{
		readonly ISession session;
		
		public AnnouncementController(ISession session) 
		{
			this.session = session;
		}
		
		public ActionResult Index()
		{
			Announcement[] announcements = session
				.Query<Announcement>()
				.OrderByDescending(a => a.AnnouncementDate)
				.ToArray();

			return View(announcements);
		}
		
		
		
		public ActionResult Create()
		{
			return View("Edit", new Announcement(){AnnouncementDate=DateTime.Today});
		}
		
		public ActionResult Edit(int id)
		{
			Announcement announcement = 
				session
					.Query<Announcement>()
					.SingleOrDefault(a => a.ID == id);
			
			if (announcement == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (announcement);
			}
		}
		
		[ValidateInput(false)]
		public ActionResult Commit(Announcement announcement)
		{
			session.SaveOrUpdate(announcement);
			TempData["message"] = String.Format("Announcement '{0}' saved.", announcement.Title);
			return RedirectToAction("Index");
		}
		
		
		public ActionResult DeleteConfirm(int id)
		{
			Announcement announcement = 
				session
					.Query<Announcement>()
					.SingleOrDefault(a => a.ID == id);
			
			if (announcement == null) {
				return RedirectToAction("Index");
			}
			else {
				return View (announcement);
			}
		}
		
		
		public ActionResult Delete(int id)
		{
			Announcement announcement = 
				session
					.Query<Announcement>()
					.SingleOrDefault(a => a.ID == id);
			
			if (announcement != null) {
				session.Delete(announcement);
				TempData["message"] = String.Format ("Announcement '{0}' removed.", announcement.Title);
			}
			
			return RedirectToAction("Index");
		}
	}
}

