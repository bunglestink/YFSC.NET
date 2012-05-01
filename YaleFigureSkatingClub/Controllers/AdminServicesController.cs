using System;
using System.Web.Mvc;
using YaleFigureSkatingClub.BusinessLayer;

namespace YaleFigureSkatingClub
{
	[Authorize(Roles="Admin")]
	public class AdminServicesController : Controller
	{
		readonly IBackupService backupService;
		
		public AdminServicesController(IBackupService backupService)
		{
			this.backupService = backupService;
		}
		
		
		public ContentResult Backup()
		{
			Response.AddHeader("Content-Disposition", "attachment;filename=backup.sql");
			return Content(backupService.getBackupScript());
		}
	}
}

