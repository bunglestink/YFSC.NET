using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaleFigureSkatingClub.Entities;
using NHibernate;
using NHibernate.Linq;

namespace YaleFigureSkatingClub.Controllers
{
	[Authorize]
    public class SkatingSessionServiceController : Controller
    {
		readonly ISession session;
		
		public SkatingSessionServiceController(ISession session)
		{
			this.session = session;
		}
        
        public JsonResult Current()
        {
            var term = session.Query<RegistrationTerm>()
				.SingleOrDefault(t => t.Current);
			
			if (term == null) {
				return Json(new List<SkatingSession>());
			}
			
			return Json(term.SkatingSessions);
        }
    }
}
