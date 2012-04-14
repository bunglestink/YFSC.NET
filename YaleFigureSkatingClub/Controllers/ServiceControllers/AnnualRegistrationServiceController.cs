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
		readonly RegistrationService registrationService;
		
		public AnnualRegistrationServiceController() 
		{
			session = NHSessionManager.CurrentSession;
			registrationService = new RegistrationService();
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
			
			registrationService.SubmitRegistration(registration);
			return Json(true);
        }
		
		
		public ActionResult GetCost(AnnualRegistration registration)
		{
            return Json(
				String.Format ("{0:c}", registrationService.GetRegistrationCost(registration)));
		}
    }
}
