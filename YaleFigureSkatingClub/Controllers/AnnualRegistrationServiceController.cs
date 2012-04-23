using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaleFigureSkatingClub.BusinessLayer;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;

namespace YaleFigureSkatingClub.Controllers
{
	[Authorize]
    public class AnnualRegistrationServiceController : Controller
    {
		readonly ISession session;
		readonly IRegistrationService registrationService;
		
		public AnnualRegistrationServiceController(ISession session, IRegistrationService registrationService) 
		{
			this.session = session;
			this.registrationService = registrationService;
		}
		
        public JsonResult New()
        {  
            return Json(new AnnualRegistration());
        }
		
		
        public JsonResult Create(AnnualRegistration registration)
        {
			if (!ModelState.IsValid) {
				var errors = ModelState
					.Where (m => m.Value.Errors.Any())
					.SelectMany (m => m.Value.Errors.Select (e => e.ErrorMessage));
					
				return Json (errors);
			}
			
			var user = session.Get<YaleFigureSkatingClub.Entities.User>(User.Identity.Name);
			registrationService.SubmitRegistration(registration, user);
			return Json(registration.Invoice.ID);
        }
		
		
		public ActionResult GetCost(AnnualRegistration registration)
		{
            return Json(
				String.Format ("{0:c}", registrationService.GetRegistrationCost(registration)));
		}
    }
}
