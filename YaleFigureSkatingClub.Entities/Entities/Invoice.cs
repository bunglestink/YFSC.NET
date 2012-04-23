using System;
using System.Collections.Generic;
using System.Linq;

namespace YaleFigureSkatingClub.Entities
{
	public class Invoice
	{
		public virtual int ID { get; set; }
		public virtual DateTime InvoiceDate { get; set; }
		public virtual AnnualRegistration Registration { get; set; }
		public virtual IList<InvoiceItem> InvoiceItems { get; set; }
		public virtual IList<InvoicePayment> InvoicePayments { get; set; }
		
		
		public virtual decimal TotalCost { get { return InvoiceItems.Sum(i => i.TotalCost); } }
		public virtual decimal AmountPaid { get { return InvoicePayments.Sum (p => p.Amount); } }
		
		public Invoice()
		{
			InvoiceItems = new List<InvoiceItem>();
			InvoicePayments = new List<InvoicePayment>();
		}
	}
}

