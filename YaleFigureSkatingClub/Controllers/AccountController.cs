using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using System.Security.Principal;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure.Logging;
using NHibernate;
using NHibernate.Linq;

namespace YaleFigureSkatingClub
{
	public class AccountController : Controller
	{	
		readonly ISession session;
		readonly ILog log;
		
		public AccountController(ISession session, ILog log)
		{
			this.session = session;
			this.log = log;
		}
		
		
		
		[ActionName("LogOn")]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult LogOn(string returnUrl)
		{
			return View (new YaleFigureSkatingClub.Entities.User());
		}
		
		
		[ActionName("LogOn")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult LogOnPost(YaleFigureSkatingClub.Entities.User user, string returnUrl)
		{
			if (ModelState.IsValid) {
                
				if (Membership.ValidateUser(user.Username, user.Password)) {
				
					log.InfoFormat ("User '{0}' logged in successfully.", user.Username);
					FormsAuthentication.SetAuthCookie(user.Username, false);	
					
                    if (String.IsNullOrEmpty(returnUrl)) {
                        return RedirectToAction("Index", "Membership");
                    }
                    else {
						return Redirect(returnUrl);
                    }
                }
                else {
					log.InfoFormat ("User '{0}' unsuccessful login attempt.", user.Username);
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
			
            // If we got this far, something failed, redisplay form
            return View("LogOn", user);
		}
		
		
		
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
		
		
		[ActionName("Create")]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Create()
		{
			var user = new YaleFigureSkatingClub.Entities.User();
			return View (user);	
		}
		
		
		[ActionName("Create")]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateSubmit(YaleFigureSkatingClub.Entities.User user) 
		{
			if (!ModelState.IsValid) {
				return View ("Create", user);
			}
			
			session.Save (user);
			FormsAuthentication.SetAuthCookie(user.Username, false);
			TempData["message"] = "Your account has been successfully created!";
			
			return RedirectToAction("Index", "Membership");
		}
	}
}