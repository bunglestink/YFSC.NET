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
    public class SkatingSessionServiceController : Controller
    {
        /// <summary>
        /// Gets all current sessions
        /// </summary>
        /// <returns></returns>
        public JsonResult Current()
        {
            var sessions = new SkatingSession[]
            {
                new SkatingSession()
                {
                    ID=1, 
                    Name="Saturday Combined MFD", 
                    StartDate=new DateTime(), 
                    DayOfWeek="Saturday", 
                    StartTime= "8:15 am",
                    EndTime= "9:35 am",
                    TotalCost=247, 
                    WeeksDuration=14,
                    Description="This is a combine class taking place on Saturdays for 14 weeks."
                },
                new SkatingSession()
                {
                    ID=2, 
                    Name="Saturday MFD (30 min)", 
                    StartDate=new DateTime(), 
                    DayOfWeek="Saturday", 
                    StartTime= "9:05 am",
                    EndTime="9:35 am",
                    TotalCost=98, 
                    WeeksDuration=14,
                    Description="This is a half hour class taking place on Saturdays for 14 weeks."
                },
                new SkatingSession()
                {
                    ID=3, 
                    Name="Sunday Bridge Class", 
                    StartDate=new DateTime(), 
                    DayOfWeek="Sunday", 
                    StartTime= "5:10 pm",
                    EndTime= "5:40 pm",
                    TotalCost=156, 
                    WeeksDuration=12,
                    Description="This bridge class takes place on sunday evenings."
                }
            };

			return Json (sessions);
        }
    }
}
