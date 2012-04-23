using System;

namespace YaleFigureSkatingClub.Entities
{
	public class InvoicePayment
	{
		public virtual int ID { get; set; }
		public virtual string Description { get; set; }
		public virtual string Type { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime DateReceived { get; set; }
		public virtual Invoice Invoice { get; set; }
	}
}

