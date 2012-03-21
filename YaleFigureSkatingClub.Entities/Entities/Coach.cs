using System;
using System.ComponentModel.DataAnnotations;

namespace YaleFigureSkatingClub.Entities
{
	public class Coach
	{
		[Required]
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual string PrimaryInfo { get; set; }
		public virtual string SecondaryInfo { get; set; }
		
		public Coach ()
		{
		}
	}
}

