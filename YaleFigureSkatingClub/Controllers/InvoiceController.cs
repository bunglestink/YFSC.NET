using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using YaleFigureSkatingClub.Entities;
using YaleFigureSkatingClub.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace Controllers
{
	[HandleError]
	[Authorize(Roles="Admin")]
	public class InvoiceController : Controller
	{
		readonly ISession session;
		
		public InvoiceController(ISession session)
		{
			this.session = session;
		}
		
		
		public ActionResult Index()
		{
			var invoices = session.Query<Invoice>()
				.OrderByDescending(x => x.ID);
			
			return View(invoices);
		}
		
		public ActionResult View(int id)
		{
			var invoice = session.Get<Invoice>(id);
			if (invoice == null) {
				return RedirectToAction("Index");
			}
			
			return View (invoice);
		}
		

		public JsonResult AddPayment(InvoicePayment invoicePayment)
		{
			var invoiceById = invoicePayment.Invoice;
			Console.WriteLine (invoicePayment.Amount);
			Console.WriteLine(invoiceById);
			Console.WriteLine(invoiceById == null ? "null" : invoiceById.ID.ToString());
			if (invoiceById == null) {
				return Json (false);
			}
			
			var invoice = session.Get<Invoice>(invoiceById.ID);
			if (invoice == null) {
				return Json (false);
			}
			
			invoicePayment.DateReceived = DateTime.Today;
			invoicePayment.Invoice = invoice;
			session.Save(invoicePayment);
			return Json(invoicePayment.ID);
		}
		
		
		public JsonResult DeletePayment(int id)
		{
			var invoicePayment = session.Get<InvoicePayment>(id);
			if (invoicePayment == null) {
				return Json(false);
			}
			session.Delete(invoicePayment);
			return Json(true);
		}
	}
}

