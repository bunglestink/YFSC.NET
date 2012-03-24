using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaleFigureSkatingClub.BusinessLayer;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using Newtonsoft.Json;
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

        public string Create(AnnualRegistration registration)
        {
            //TODO: verify that user isn't already registered, save to DB, run invoice
            return "successfully registered";
        }
		
		
		public ActionResult GetCost(AnnualRegistration registration)
		{
			return Json(registrationService.GetRegistrationCost(registration));
		}
    }
}
