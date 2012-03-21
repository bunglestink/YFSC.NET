using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaleFigureSkatingClub.Entities;
using Newtonsoft.Json;

namespace YaleFigureSkatingClub.Controllers
{
	[Authorize]
    public class AnnualRegistrationServiceController : Controller
    {
        public JsonResult New()
        {  
            return Json(new AnnualRegistration());
			//return JsonConvert.SerializeObject(new AnnualRegistration());
        }

        public string Create(AnnualRegistration registration)
        {
            //TODO: verify that user isn't already registered, save to DB, run invoice
            return "successfully registered";
        }
    }
}
