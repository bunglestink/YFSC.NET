using System;

namespace YaleFigureSkatingClub.Entities
{
	public class InvoiceItem
	{

		public virtual int ID { get; set; }
		public virtual string Description { get; set; }
		public virtual decimal UnitCost { get; set; }
		public virtual int Quantity { get; set; }
		public virtual Invoice Invoice { get; set; }
		
		
		public virtual decimal TotalCost { get { return UnitCost * Quantity; } }
	}
}

