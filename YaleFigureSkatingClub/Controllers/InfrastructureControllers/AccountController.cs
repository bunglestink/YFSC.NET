using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Security;
using System.Security.Principal;
using YaleFigureSkatingClub.Entities;

namespace YaleFigureSkatingClub
{
	public class AccountController : Controller
	{	
		public ActionResult LogOn(User user, string returnUrl)
		{
			if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Username, user.Password))
                {
					FormsAuthentication.SetAuthCookie(user.Username, false);	
					
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "Membership");
                    }
                    else
                    {
						return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(user);
		}
		
		
		
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Index", "Home");
		}
		
		
		
		public ActionResult Create(User user)
		{
			ModelState.AddModelError("donkey", "Error!!!!!");
			return View (user);	
		}
	}
}